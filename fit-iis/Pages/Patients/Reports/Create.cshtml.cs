/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iis_project.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace iis_project.Pages.Patients.Reports
{
    [Authorize(Roles = RolesMetadata.ADMIN + "," + RolesMetadata.DOCTOR)]
    public class CreateModel : PageModel
    {
        private ApplicationDbContext _dbCtx;
        private UserManager<ApplicationUser> _userManager;
        private IWebHostEnvironment _hostEnv;
        private MedicalTicket Ticket { get; set; }

        public CreateModel(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _dbCtx = dbContext;
            _userManager = userManager;
            _hostEnv = environment;
        }

        public class InputModel
        {
            [Display(Name = "Nadpis")]
            public string Header { get; set; }
            [Display(Name = "Obsah")]
            public string Content { get; set; }
            [Display(Name = "Obrázky")]
            public IFormFile[] FileUploads { get; set; } = new IFormFile[0];
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        private async Task<ActionResult> _LoadAndCheck(int? recordid = null, int? ticketid = null)
        {
            if (recordid != null)
            {
                // Load record
                var rec = await  _dbCtx.MedicalRecords.Include(r => r.Doctor).FirstOrDefaultAsync(r => r.MedicalRecordId == recordid);
                if (rec == null) return NotFound();
                // Is doctor permitted to create in this?
                if (User.IsInRole(RolesMetadata.DOCTOR) && User.Identity.Name != rec.Doctor.UserName) return Forbid();
            }
            else if (ticketid != null)
            {
                // Load ticket
                Ticket = await _dbCtx.MedicalTickets.Include(t => t.Doctor).Include(t => t.Record).FirstOrDefaultAsync(r => r.MedicalTicketId == ticketid);
                if (Ticket == null) return NotFound();
                // Is doctor permitted to create in this?
                if (User.IsInRole(RolesMetadata.DOCTOR) && User.Identity.Name != Ticket.Doctor.UserName) return Forbid();
            }
            else
            {
                return NotFound();
            }

            return null; // Success
        }

        public async Task<ActionResult> OnGet(int? recordid = null, int? ticketid = null)
        {
            var chckresult = await _LoadAndCheck(recordid, ticketid);
            if (chckresult != null) return chckresult;

            return Page();
        }

        public async Task<ActionResult> OnPostAsync(int? recordid = null, int? ticketid = null)
        {
            var chckresult = await _LoadAndCheck(recordid, ticketid);
            if (chckresult != null) return chckresult;

            if (ModelState.IsValid)
            {
                MedicalReport medicalReport = new MedicalReport()
                {
                    Content = Input.Content,
                    Header = Input.Header,
                    MedicalTicketId = ticketid,
                    MedicalRecordId = recordid,
                    CreatedBy = await _userManager.GetUserAsync(User),
                    Images = new List<ReportImage>()
                };

                // File uploads
                foreach (var f in Input.FileUploads)
                {
                    var id = Guid.NewGuid();
                    string path = Path.Combine(_hostEnv.WebRootPath, "ReportImages", id + "_" + f.FileName);
                    using (var fs = new FileStream(path, FileMode.Create))
                    {
                        await f.CopyToAsync(fs);
                    }
                    medicalReport.Images.Add(new ReportImage() { Id = id.ToString(), Name = f.FileName });

                }

                _dbCtx.MedicalReports.Add(medicalReport);
                await _dbCtx.SaveChangesAsync();
                return (ticketid != null) ? 
                    RedirectToPage("./TicketReportBrowser", new { patientid = RouteData.Values["patientid"], recordid = Ticket.Record.MedicalRecordId, ticketid = ticketid, selectedReportId = medicalReport.MedicalReportId } ) :
                    RedirectToPage("./RecordReportBrowser", new { patientid = RouteData.Values["patientid"], recordid = recordid, selectedReportId = medicalReport.MedicalReportId }); 
            }

            return Page();

        }
    }
}
