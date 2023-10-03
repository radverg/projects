/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using iis_project.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using iis_project.Data.Enums;

namespace iis_project.Pages.Patients.Reports
{
    [Authorize(Roles = RolesMetadata.ADMIN + "," + RolesMetadata.DOCTOR + "," + RolesMetadata.PATIENT)]
    public class TicketReportBrowserModel : PageModel
    {
        private readonly ApplicationDbContext _dbCtx;

        public MedicalTicket Ticket { get; set; }
        public IEnumerable<MedicalReport> Reports { get; set; }
        public MedicalReport SelectedReport { get; set; }
        public List<TicketAct> TicketActs { get; set; }
        public IEnumerable<SelectListItem> ActItems { get; set; }
        public bool Editable { get => (User.IsInRole(RolesMetadata.ADMIN) || User.IsInRole(RolesMetadata.DOCTOR)) && Ticket.Status == StatusTicket.Open;}
        public bool OwnRecord { get => User.Identity.Name == Ticket?.Record?.Doctor?.UserName || User.Identity.Name == Ticket.Record.Patient.UserName; }

        public TicketReportBrowserModel(ApplicationDbContext applicationDbContext)
        {
            _dbCtx = applicationDbContext;
        }

        private async Task _Load(int ticketid, int? selectedReportId = null)
        {
            Reports = await _dbCtx.MedicalReports
               .Include(r => r.Images)
               .OrderByDescending(r => r.DtCreated)
               .Where(r => r.MedicalTicketId == ticketid)
               .ToListAsync();

            if (selectedReportId != null)
                SelectedReport = Reports.First(r => r.MedicalReportId == selectedReportId);
            else if (Reports.Count() > 0)
            {
                SelectedReport = Reports.First();
                selectedReportId = SelectedReport.MedicalReportId;
            }

            Ticket = await _dbCtx.MedicalTickets
                .Include(t => t.Doctor)
                .Include(t => t.Record)
                    .ThenInclude(r => r.Patient)
                .Include(t => t.Record.Doctor)
                .FirstAsync(t => t.MedicalTicketId == ticketid);

            TicketActs = await _dbCtx.TicketActs
                .Include(ta => ta.MedicalAct)
                .Where(ta => ta.MedicalTicketId == ticketid)
                .OrderBy(ta => ta.Status)
                    .ThenByDescending(ta => ta.DtCreated)
                .ToListAsync();

            ActItems = (await _dbCtx.InsuranceActs.Where(a => a.Active == true).ToListAsync())
                .Select( t => new SelectListItem(t.Name, t.MedicalActId.ToString()) );

        }

        public async Task<ActionResult> OnGetAsync(int ticketid, int? selectedReportId = null)
        {
            try
            {
                await _Load(ticketid, selectedReportId);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            if (!User.IsInRole(RolesMetadata.ADMIN) && User.IsInRole(RolesMetadata.DOCTOR) && Ticket.Doctor.UserName != User.Identity.Name && Ticket.Record.Doctor.UserName != User.Identity.Name)
                return Forbid();
            if (!User.IsInRole(RolesMetadata.ADMIN) && User.IsInRole(RolesMetadata.PATIENT) && Ticket.Record.Patient.UserName != User.Identity.Name)
                return Forbid();

            return Page();
        }

        [BindProperty]
        [Required(ErrorMessage = "Je třeba zvolit úkon.")]
        public int ActId { get; set; }

        public async Task<ActionResult> OnPostAsync(int ticketid)
        {
            // Handle new pay request
            if (ModelState.IsValid)
            {
                MedicalTicket ticket = await _dbCtx.MedicalTickets.FirstOrDefaultAsync(t => t.MedicalTicketId == ticketid);
                if (ticket == null) return NotFound();
                _dbCtx.TicketActs.Add(new TicketAct()
                {
                    MedicalTicketId = ticketid,
                    MedicalActId = ActId
                });

                await _dbCtx.SaveChangesAsync();

                return RedirectToPage("./TicketReportBrowser", RouteData.Values);
            }

            return Page();
        }
    }
}
