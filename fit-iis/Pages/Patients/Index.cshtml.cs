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

namespace iis_project.Pages.Patients
{
    [Authorize(Roles = RolesMetadata.DOCTOR + "," + RolesMetadata.ADMIN)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbCtx;
        public List<ApplicationUser> Patients { get; set; } = new List<ApplicationUser>();

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbCtx = dbContext;
        }
        public void OnGet()
        {
            Patients = _dbCtx.Users.Include(x => x.Roles)
                                   .Where(x => x.Roles.Select(y => y.RoleId).Contains(RolesMetadata.PATIENT))
                                   .OrderBy(x => x.Surname)
                                   .ThenBy(x => x.GivenName)
                                   .ToList();
        }
    }
}
