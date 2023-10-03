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
using Microsoft.AspNetCore.Mvc.RazorPages;
using iis_project.Data.Enums;

namespace iis_project.Pages.Patients.Reports
{
    [Authorize(Roles = RolesMetadata.ADMIN + "," + RolesMetadata.DOCTOR + "," + RolesMetadata.PATIENT)]
    public class RecordReportBrowserModel : PageModel
    {
        private readonly ApplicationDbContext _dbCtx;

        public MedicalRecord Record { get; set; }
        public IEnumerable<MedicalReport> Reports { get; set; }
        public IEnumerable<MedicalTicket> Tickets { get; set; }
        public MedicalReport SelectedReport { get; set; }

        public bool Editable { get => (User.IsInRole(RolesMetadata.ADMIN) || User.IsInRole(RolesMetadata.DOCTOR)) && Record.Status == StatusRecord.Open; }

        public RecordReportBrowserModel(ApplicationDbContext applicationDbContext)
        {
            _dbCtx = applicationDbContext;
        }

        private async Task _Load(int recordid, int? selectedReportId = null)
        {
            Record = await _dbCtx.MedicalRecords
                .Include(t => t.Doctor)
                .Include(t => t.Patient)
                .Include(t => t.MedicalTickets)
                .Include(t => t.MedicalReports)
                    .ThenInclude(r => r.Images)
                .FirstAsync(t => t.MedicalRecordId == recordid);

            Reports = Record.MedicalReports.OrderByDescending(r => r.DtCreated);
            Tickets = Record.MedicalTickets.OrderByDescending(r => r.DtCreated);

            if (selectedReportId != null)
                SelectedReport = Reports.First(r => r.MedicalReportId == selectedReportId);
            else if (Reports.Count() > 0)
            {
                SelectedReport = Reports.First();
                selectedReportId = SelectedReport.MedicalReportId;
            }
        }

        public async Task<ActionResult> OnGetAsync(int recordid, int? selectedReportId = null)
        {
            try
            {
                await _Load(recordid, selectedReportId);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            if (!User.IsInRole(RolesMetadata.ADMIN) && User.IsInRole(RolesMetadata.DOCTOR) && Record.Doctor.UserName != User.Identity.Name)
                return Forbid();
            if (!User.IsInRole(RolesMetadata.ADMIN) && User.IsInRole(RolesMetadata.PATIENT) && Record.Patient.UserName != User.Identity.Name)
                return Forbid();

            return Page();
        }
    }
}
