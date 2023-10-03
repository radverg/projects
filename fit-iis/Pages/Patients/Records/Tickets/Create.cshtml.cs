/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using iis_project.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace iis_project.Pages.Patients.Tickets
{
    [Authorize(Roles = RolesMetadata.ADMIN + "," + RolesMetadata.DOCTOR)]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _dbCtx;
        private readonly UserManager<ApplicationUser> _userManager;

        public IEnumerable<SelectListItem> DoctorsItems { get; set; }

        [BindProperty]
        public InputModel TicketInput { get; set; }

        public class InputModel
        {
            [Display(Name = "Ošetřující lékař")]
            [Required(ErrorMessage = "Ticket musí mít ošetřujícího lékaře.")]
            public string Doctor { get; set; }
            [Display(Name = "Popis vyšetření")]
            public string Description { get; set; }
        }

        public CreateModel(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbCtx = dbContext;
            _userManager = userManager;
        }

        private async Task _Load()
        {
            var doctors = await _dbCtx.Users
                .Include(x => x.Roles)
                .Where(x => x.Roles
                    .Select(y => y.RoleId)
                    .Contains(RolesMetadata.DOCTOR))
                .ToListAsync();
            DoctorsItems = doctors.Select(d => new SelectListItem(d.Fullname, d.UserName));
            string seldocname = TicketInput?.Doctor ?? User.Identity.Name;
            var seldoc = DoctorsItems.FirstOrDefault(d => d.Value == seldocname);
            if (seldoc != null) seldoc.Selected = true;
        }

        public async Task<IActionResult> OnGetAsync(int recordid)
        {
            // Check if doctor has rights to create ticket on this record
            MedicalRecord tmp = await _dbCtx.MedicalRecords.Include(r => r.Doctor).FirstOrDefaultAsync(r => r.MedicalRecordId == recordid);
            if (tmp == null) return NotFound();
            if (!User.IsInRole(RolesMetadata.ADMIN) && tmp.Doctor.UserName != User.Identity.Name)
                return Forbid();

            await _Load();
            TicketInput = new InputModel();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int recordid)
        {
            if (ModelState.IsValid)
            {
                MedicalTicket tic = new MedicalTicket();
                tic.CreatedBy = await _userManager.FindByNameAsync(User.Identity.Name);
                tic.Description = TicketInput.Description;
                tic.Doctor = await _userManager.FindByNameAsync(TicketInput.Doctor);
                tic.Status = Data.Enums.StatusTicket.Open;
                tic.Record = await _dbCtx.MedicalRecords.FindAsync(recordid);

                if (tic.Record == null) return NotFound();

                _dbCtx.MedicalTickets.Add(tic);
                await _dbCtx.SaveChangesAsync();
                return RedirectToPage("/Patients/Reports/RecordReportBrowser", new { patientid = RouteData.Values["patientid"], recordid = recordid });
            }

            return Page();
        }
    }
}
