/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using iis_project.Data;

namespace iis_project.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel LoginInput { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Zadejte prosím váš email.")]
            [EmailAddress(ErrorMessage = "Emailová adresa nemá správný formát.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Zadejte prosím vaše heslo.")]
            [Display(Name = "Heslo")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Pamatovat přihlášení")]
            public bool RememberMe { get; set; }
        }

        public void OnGetAsync()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(LoginInput.Email, LoginInput.Password, LoginInput.RememberMe, lockoutOnFailure: false);
             
                if (result.Succeeded)
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Přihlášení se nezdařilo.");
                    return Page();
                }
            }

            return Page();
        }
    }
}
