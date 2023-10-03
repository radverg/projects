/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using System;
using iis_project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace iis_project.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public ActionResult OnGet()
        {
            // Redirect to default pages according to the user's role
            if (User.IsInRole(RolesMetadata.ADMIN))
            {
                // Admin will most likely manage accounts.
                return RedirectToPage("/Accounts/Index");
            }

            if (User.IsInRole(RolesMetadata.INSURANCE_EMPLOYEE))
            {
                // Insurance employee will probably want to confirm requests from doctor
                return RedirectToPage("/Insurance/Index");
            }

            if (User.IsInRole(RolesMetadata.DOCTOR))
            {
                // Doctor will most likely want to select patient to further work with
                return RedirectToPage("/Patients/Index");
            }

            if (User.IsInRole(RolesMetadata.PATIENT))
            {
                // Patient will most likely see overview of his health documentation
                return RedirectToPage($"/Patients/Records/Index", new { patientid = User.Identity.Name });

            }

            // User does not have any role? keep it here, then.
            return Page();
        }
    }
}
