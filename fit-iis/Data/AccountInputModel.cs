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

namespace iis_project.Data
{ 
    public class AccountInputModel
    {
        [Required(ErrorMessage = "Email je povinné pole.")]
        [EmailAddress(ErrorMessage = "Zadaný email nemá správný formát.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "{0} musí mít alespoň {2} a maximálně {1} znaků.")]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Display(Name = "Heslo znovu")]
        [Compare("Password", ErrorMessage = "Hesla se neshodují.")]
        public string ConfirmPassword { get; set; } = "";

        [DataType(DataType.Date)]
        [Display(Name = "Datum narození")]
        [Required(ErrorMessage = "Datum narození je povinné pole.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Jméno je povinné pole.")]
        [Display(Name = "Jméno")]
        [StringLength(50, ErrorMessage = "{0} musí mít alespoň {2} a maximálně {1} znaků.", MinimumLength = 2)]
        public string GivenName { get; set; }
        [Required(ErrorMessage = "Příjmení je povinné pole")]
        [Display(Name = "Příjmení")]
        [StringLength(60, ErrorMessage = "{0} musí mít alespoň {2} a maximálně {1} znaků.", MinimumLength = 2)]
        public string Surname { get; set; }

        [Display(Name = "Uživatelské role")]
        public List<string> Roles { get; set; } = new List<string>();

        public static AccountInputModel FromApplicationUser(ApplicationUser applicationUser) 
        {
            AccountInputModel result = new AccountInputModel()
            {
                Email = applicationUser.Email,
                BirthDate = applicationUser.BirthDate,
                GivenName = applicationUser.GivenName,
                Surname = applicationUser.Surname,
                Roles = applicationUser.Roles.Select(r => r.RoleId).ToList()
            };

            return result;
        }
    }
}
