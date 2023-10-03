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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using iis_project.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using iis_project.Data.Enums;

namespace iis_project.Pages.Insurance
{
    [Authorize(Roles = RolesMetadata.INSURANCE_EMPLOYEE + "," + RolesMetadata.ADMIN)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbCtx;
        public List<TicketAct> TicketActs { get; set; } = new List<TicketAct>();

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbCtx = dbContext;
        }

        public void OnGet()
        {
            TicketActs = _dbCtx.TicketActs.Include(x => x.MedicaTicket)
                                          .Include(x => x.MedicalAct)
                                          .Include(x => x.MedicaTicket.Doctor)
                                          .Include(x => x.MedicaTicket.Record)
                                          .Include(x => x.MedicaTicket.Record.Patient)
                                          .OrderBy(x => x.DtCreated)
                                          .Where(x => x.Status == StatusInsurance.Open)
                                          .ToList();
        }

        public async Task<ActionResult> OnPostConfirm(int ticketactid)
        {
            TicketAct act = await _dbCtx.TicketActs.FirstOrDefaultAsync(x => x.TicketActId == ticketactid);
            if (act == null) return NotFound();

            if (act.Status == StatusInsurance.Open)
                act.Status = StatusInsurance.Accepted;
            await _dbCtx.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task<ActionResult> OnPostDecline(int ticketactid)
        {
            TicketAct act = await _dbCtx.TicketActs.FirstOrDefaultAsync(x => x.TicketActId == ticketactid);
            if (act == null) return NotFound();

            if (act.Status == StatusInsurance.Open)
                act.Status = StatusInsurance.Rejected;
            await _dbCtx.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
