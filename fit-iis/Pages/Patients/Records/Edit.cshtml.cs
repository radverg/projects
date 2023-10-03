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
using iis_project.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace iis_project.Pages.Patients
{
    [Authorize(Roles = RolesMetadata.ADMIN + "," + RolesMetadata.DOCTOR)]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _dbCtx;
        private readonly UserManager<ApplicationUser> _userManager;

        public List<ApplicationUser> Doctors { get; set; } = new List<ApplicationUser>();
        public ApplicationUser Doctor { get; set; }

        [BindProperty]
        public InputModel RecordInput { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Název záznamu je povinný.")]
            [Display(Name = "Název")]
            public string Name { get; set; }

            [Display(Name = "Popis")]
            public string Description { get; set; }

            [Display(Name = "Spravující lékař: ")]
            [Required(ErrorMessage = "Záznam musí mít spravujícího lékaře.")]
            public string Doctor { get; set; }
        }

        public EditModel(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbCtx = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int? recordid = null)
        {
            RecordInput = new InputModel();
            Doctor = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!User.IsInRole(RolesMetadata.DOCTOR))
                Doctor = null;

            Doctors = await _dbCtx.Users
                .Include(x => x.Roles)
                .Where(x => x.Roles
                    .Select(y => y.RoleId)
                    .Contains(RolesMetadata.DOCTOR))
                .Where(x => x.UserName != User.Identity.Name)
                .ToListAsync();

            if (recordid != null)
            {
                MedicalRecord Inp = await _dbCtx.MedicalRecords.Include(x => x.Doctor).FirstOrDefaultAsync(mr => mr.MedicalRecordId == recordid);
                if (Inp == null || Inp.Doctor == null) return NotFound();
                if (!User.IsInRole(RolesMetadata.ADMIN) && Inp.Doctor.UserName != User.Identity.Name)
                    return Forbid();
                RecordInput.Name = Inp.Name;
                RecordInput.Description = Inp.Description;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string patientid, int? recordid = null)
        {
            if (ModelState.IsValid)
            {
                if (recordid != null)
                {
                    // Record id is given - editing
                    MedicalRecord rec = await _dbCtx.MedicalRecords.FirstOrDefaultAsync(r => r.MedicalRecordId == recordid);
                    if (rec == null) return NotFound();

                    rec.Name = RecordInput.Name;
                    rec.Description = RecordInput.Description;
                    rec.Doctor = await _userManager.FindByNameAsync(RecordInput.Doctor);
                }
                else
                {
                    // REcord id not given - creating
                    MedicalRecord added = new MedicalRecord();
                    added.Name = RecordInput.Name;
                    added.Description = RecordInput.Description;
                    added.Patient = await _userManager.FindByNameAsync(patientid);
                    if (added.Patient == null) return NotFound();
                    added.Doctor = await _userManager.FindByNameAsync(RecordInput.Doctor);
                    added.Status = StatusRecord.Open;

                    _dbCtx.MedicalRecords.Add(added);
                    int result = await _dbCtx.SaveChangesAsync();
                }

                await _dbCtx.SaveChangesAsync();
                return RedirectToPage("./Index", new { patientid = patientid });
            }

            return Page();

        }
    }
}
