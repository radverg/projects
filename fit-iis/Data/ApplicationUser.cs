/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
namespace iis_project.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Uživatelské role")]
        public virtual List<IdentityUserRole<string>> Roles { get; set; }

        [NotMapped]
        [Display(Name = "Jméno a příjmení")]
        public string Fullname { get => GivenName + " " + Surname; }

        [DataType(DataType.Date)]
        [Display(Name = "Datum narození")]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Datum vytvoření")]
        public DateTime DtCreated { get; set; }

        [Display(Name = "Křestní jméno")]
        public string GivenName { get; set; }

        [Display(Name = "Příjmení")]
        public string Surname { get; set; }

       

        //public List<MedicalRecord> CreatedMedicalRecords { get; set; }
        //public List<MedicalReport> CreatedMedicalReports { get; set; }
        //public List<MedicalTicket> CreatedTickets { get; set; }
        //public List<MedicalTicket> ActiveTickets { get; set; }


        public static string GetFullName(ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.GivenName) + " " + claimsPrincipal.FindFirstValue(ClaimTypes.Surname);
        }
    }

    class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
                : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.GivenName));
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.Surname));
            identity.AddClaim(new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString()));
            return identity;
        }
    }
}
