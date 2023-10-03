/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using iis_project.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace iis_project.Pages.Patients.Tickets
{
    [Authorize(Roles = RolesMetadata.DOCTOR + "," + RolesMetadata.ADMIN)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbCtx;
        public List<MedicalTicket> Tickets { get; set; } = new();

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbCtx = dbContext;
        }
        public void OnGet()
        {
            Tickets = _dbCtx.MedicalTickets
                .Include(x => x.Doctor)
                .Include(x => x.Record)
                    .ThenInclude(r => r.Patient)
                .Include(x => x.CreatedBy)
                .OrderByDescending(t => t.DtCreated)
                .Where(x => x.Doctor.UserName == User.Identity.Name)
                .ToList();
        }
    }
}
