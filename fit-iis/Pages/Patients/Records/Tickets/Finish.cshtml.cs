/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using System;
using System.Linq;
using iis_project.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace iis_project.Pages.Patients.Records.Tickets
{
    [Authorize(Roles = RolesMetadata.ADMIN + "," + RolesMetadata.DOCTOR)]
    public class FinishModel : PageModel
    {
        private ApplicationDbContext _dbCtx;

        public FinishModel(ApplicationDbContext applicationDbContext)
        {
            _dbCtx = applicationDbContext;
        }

        public IActionResult OnGet(int ticketid, string returnUrl)
        {
            var tic = _dbCtx.MedicalTickets
                .Include(mr => mr.Doctor)
                .FirstOrDefault(m => m.MedicalTicketId == ticketid);

            if (tic == null) return NotFound();
            if (!User.IsInRole(RolesMetadata.ADMIN) && (User.IsInRole(RolesMetadata.DOCTOR) && tic.Doctor.UserName != User.Identity.Name))
                return Forbid();

            tic.Status = Data.Enums.StatusTicket.Closed;
            _dbCtx.SaveChanges();
            return LocalRedirect(returnUrl);
        }
    }
}
