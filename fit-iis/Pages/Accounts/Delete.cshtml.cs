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
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace iis_project.Pages.Accounts
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _dbctx;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUser DeleteTarget { get; set; }

        public List<MedicalRecord> TransferRecords { get; set; }
        public List<MedicalTicket> MarkTickets { get; set; }

        public List<SelectListItem> AvailableDoctors { get; set; }

        public DeleteModel(ApplicationDbContext dbContext, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _dbctx = dbContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public class InputModel
        {
            [Display(Name = "Převést záznamy k lékaři")]
            public string TransferToDoctorId { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        private bool _IsForbidden(string id) => (!User.IsInRole(RolesMetadata.ADMIN) && id != User.Identity.Name);

        private async Task _LoadDeleteTarget(string username)
        {
            DeleteTarget = await _dbctx.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (DeleteTarget == null)
            {
                ModelState.AddModelError(string.Empty, $"Nebyl nalezen uživatel s zadaným emailem.");
                return;
            }

            // Insurance - we dont care, just delete
            // Admin - we dont care, just delete
            // Patient - records are deleted automatically by db cascading
            // Doctor - problem - transfer patient records to other doctor, check if doctor is responsible for some reports
            TransferRecords = await _dbctx.MedicalRecords
                .Include(m => m.Doctor)
                .Where(m => m.Doctor.UserName == DeleteTarget.UserName)
                .ToListAsync();

            MarkTickets = await _dbctx.MedicalTickets
                .Include(t => t.Doctor)
                .Where(t => t.Doctor.UserName == DeleteTarget.UserName)
                .ToListAsync();

            AvailableDoctors = _dbctx.Users
                .Include(u => u.Roles)
                .Where(u => u.Roles.Any(r => r.RoleId == RolesMetadata.DOCTOR))
                .Select(u => new SelectListItem(u.Fullname, u.UserName))
                .ToList();

            AvailableDoctors.RemoveAll(d => d.Value == DeleteTarget.UserName);
        }


        public async Task<ActionResult> OnGetAsync(string username)
        {
            await _LoadDeleteTarget(username);
            if (DeleteTarget == null)
                return NotFound();
            if (_IsForbidden(DeleteTarget.UserName)) return Forbid();

            return Page();
        }

        public async Task<ActionResult> OnPostAsync(string username)
        {
            await _LoadDeleteTarget(username);
            if (DeleteTarget == null)
                return NotFound();
            if (_IsForbidden(DeleteTarget.UserName)) return Forbid();

            // Transfer to the doctor
            if (TransferRecords.Count > 0)
            {
                ApplicationUser targetDoctor = await _dbctx.Users.FirstOrDefaultAsync(u => u.UserName == Input.TransferToDoctorId);
                if (targetDoctor == null) return NotFound();
                TransferRecords.ForEach(mr => mr.Doctor = targetDoctor);
                await _dbctx.SaveChangesAsync();
            }

            // Manual deletion since db cannot handle multiple cascade paths
            var todel = await _dbctx.MedicalRecords
                .Include(mr => mr.MedicalReports)
                .Include(mr => mr.Patient)
                .Where(mr => mr.Patient == DeleteTarget)
                .ToListAsync();

            // Delete reports and records, rest is handled by the db
            todel.ForEach(mr => _dbctx.MedicalReports.RemoveRange(mr.MedicalReports));
            _dbctx.MedicalRecords.RemoveRange(todel);
            await _dbctx.SaveChangesAsync();

            // Now transfer tickets
            await _dbctx.MedicalTickets
                .Include(t => t.Doctor)
                .Include(t => t.Record)
                    .ThenInclude(r => r.Doctor)
                .Where(t => t.Doctor.UserName == DeleteTarget.UserName)
                .ForEachAsync(t => t.Doctor = t.Record.Doctor);

            await _dbctx.SaveChangesAsync();

            if (DeleteTarget.UserName == User.Identity.Name)
            {
                // We are delting the user that is signed in.
                await _signInManager.SignOutAsync();
            }

            _dbctx.Users.Remove(DeleteTarget);
            await _dbctx.SaveChangesAsync();


            return RedirectToPage("/Index");
        }
    }
}
