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
using iis_project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace iis_project.Pages.Patients
{
    [Authorize(Roles = RolesMetadata.ADMIN + "," + RolesMetadata.DOCTOR + "," + RolesMetadata.PATIENT)]
    public class RecordsModel : PageModel
    {
        private readonly ApplicationDbContext _dbCtx;
        public List<MedicalRecord> Records { get; set; } = new List<MedicalRecord>();
        public bool EditPermission { get => User.IsInRole(RolesMetadata.ADMIN) || User.IsInRole(RolesMetadata.DOCTOR); }

        public RecordsModel(ApplicationDbContext dbContext)
        {
            _dbCtx = dbContext;
        }
        public async Task<ActionResult> OnGet(string patientid)
        {
            if (User.IsInRole(RolesMetadata.PATIENT) && User.Identity.Name != patientid)
            {
                return Forbid();
            }

            var recordsTmp = _dbCtx.MedicalRecords
                .Include(x => x.Doctor)
                .Include(x => x.Patient)
                .OrderBy(x => x.Status)
                .ThenByDescending(x => x.DtCreated)
                .Where(x => x.Patient.UserName == patientid);

            
            if (User.IsInRole(RolesMetadata.DOCTOR) && User.Identity.Name != patientid)
            {
                // Doctor looking on someone's records - show only these created by the doctor
                recordsTmp = recordsTmp.Where(x => x.Doctor.UserName == User.Identity.Name);
            }

            Records = await recordsTmp.ToListAsync();
            return Page();
        }
    }
}
