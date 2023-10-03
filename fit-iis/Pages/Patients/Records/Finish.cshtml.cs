/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using System;
using System.Linq;
using iis_project.Data;
using iis_project.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace iis_project.Pages.Patients.Records
{
    [Authorize(Roles = RolesMetadata.ADMIN + "," + RolesMetadata.DOCTOR)]
    public class FinishModel : PageModel
    {
        private ApplicationDbContext _dbCtx;
        public bool UnableToFinish = false;

        public FinishModel(ApplicationDbContext applicationDbContext)
        {
            _dbCtx = applicationDbContext;
            UnableToFinish = false;
        }

        public IActionResult OnGet(int recordid, string returnUrl)
        {
            var rec = _dbCtx.MedicalRecords
                .Include(mr => mr.Doctor)
                .Include(mr => mr.MedicalTickets)
                .FirstOrDefault(m => m.MedicalRecordId == recordid);

            if (rec == null) return NotFound();
            if (!User.IsInRole(RolesMetadata.ADMIN) && (User.IsInRole(RolesMetadata.DOCTOR) && rec.Doctor.UserName != User.Identity.Name))
                return Forbid();

            foreach (MedicalTicket t in rec.MedicalTickets)
            {
                if (t.Status == StatusTicket.Open)
                {
                    UnableToFinish = true;
                    return Page();
                }
             }

            rec.Status = Data.Enums.StatusRecord.Closed;
            _dbCtx.SaveChanges();
            return LocalRedirect(returnUrl);
        }
    }
}
