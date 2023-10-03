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

namespace iis_project.Pages.Insurance
{
    [Authorize(Roles = RolesMetadata.INSURANCE_EMPLOYEE + "," + RolesMetadata.ADMIN)]
    public class ActsModel : PageModel
    {
        private readonly ApplicationDbContext _dbCtx;

        public List<MedicalAct> ActiveActs { get; set; } = new List<MedicalAct>();
        public List<MedicalAct> DeactiveActs { get; set; } = new List<MedicalAct>();

        public ActsModel(ApplicationDbContext dbContext)
        {
            _dbCtx = dbContext;
        }

        public void OnGet()
        {
            ActiveActs = _dbCtx.InsuranceActs.OrderBy(x => x.Name)
                                             .Where(x => x.Active == true)
                                             .ToList();
            DeactiveActs = _dbCtx.InsuranceActs.OrderBy(x => x.Name)
                                             .Where(x => x.Active == false)
                                             .ToList();
        }

        public async Task<ActionResult> OnPostDelete(int actid)
        {
            MedicalAct act = await _dbCtx.InsuranceActs.FirstOrDefaultAsync(x => x.MedicalActId == actid);
            if (act == null) return NotFound();

            act.Active = false;
            await _dbCtx.SaveChangesAsync();

            return RedirectToPage("./Acts");
        }
    }
}
