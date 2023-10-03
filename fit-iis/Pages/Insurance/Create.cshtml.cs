/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using iis_project.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace iis_project.Pages.Insurance
{
    [Authorize(Roles = RolesMetadata.INSURANCE_EMPLOYEE + "," + RolesMetadata.ADMIN)]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _dbCtx;

        [BindProperty]
        public InputModel ActInput { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Pole Cena je povinné.")]
            [Display(Name = "Název")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Pole Cena je povinné.")]
            [Range((double) decimal.Zero, (double) decimal.MaxValue, ErrorMessage = "Cena musí být kladná!")]
            [Display(Name = "Cena")]
            public decimal Price { get; set; }

            [Display(Name = "Popis")]
            public string Description { get; set; }
        }

        public CreateModel(ApplicationDbContext dbContext)
        {
            _dbCtx = dbContext;
        }

        public void OnGetAsync()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                MedicalAct act = new MedicalAct();
                act.Name = ActInput.Name;
                act.Description = ActInput.Description;
                act.Price = ActInput.Price;
                act.Active = true;

                _dbCtx.InsuranceActs.Add(act);
                int result = await _dbCtx.SaveChangesAsync();

                if (result != 0)
                {
                    return RedirectToPage("./Acts");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Vytváření úkonu se nezdařilo.");
                    return Page();
                }
            }

            return Page();
        }
    }
}
