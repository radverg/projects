/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using iis_project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace iis_project.Pages.Accounts
{
    [Authorize(Roles = RolesMetadata.ADMIN)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbCtx;


        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbCtx = dbContext;
        }

        public void OnGet()
        {
            // Load users from db
            Users = _dbCtx.Users.Include(x => x.Roles).ToList();
        }
    }
}
