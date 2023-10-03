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
using iis_project.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace iis_project.Pages.Accounts
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _dbctx;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public EditModel(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _dbctx = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public AccountInputModel Input { get; set; }

        /// <summary>
        /// Indicates whether user name parameter is missing, if so, this page is intended to create new user
        /// </summary>
        public bool NewUser { get => string.IsNullOrEmpty((string)RouteData.Values.GetValueOrDefault("username", "")); }
       
        public ActionResult OnGet(string username = null)
        {
            if (_IsForbidden(username)) return Forbid();

            Input = LoadInputModelById(username);
            if (Input == null) return NotFound();
            return Page();
        }

        private AccountInputModel LoadInputModelById(string username)
        {
            if (string.IsNullOrEmpty(username)) return new AccountInputModel();
            ApplicationUser u = _dbctx.Users.Include(u => u.Roles).FirstOrDefault(u => u.UserName == username);
            if (u == null) return null;
            return AccountInputModel.FromApplicationUser(u);
        }

        // User is not the admin and is attempting to either create new user or modify someone's else account
        private bool _IsForbidden(string id) => ( !User.IsInRole(RolesMetadata.ADMIN) && (NewUser || id != User.Identity.Name) );

        public async Task<ActionResult> OnPostAsync(string username)
        {
            if (!ModelState.IsValid)
            {
              //  Input = LoadInputModelById(id);
                return Page();
            }

            ApplicationUser user; // User that we are working with
            bool selfedit = false;

            // Check permissions
            if (_IsForbidden(username)) return Forbid();

            if (NewUser)
            {
                // No id - creating new user
                user = new ApplicationUser()
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                };

                var createResult = await _userManager.CreateAsync(user, Input.Password);
                if (!createResult.Succeeded)
                {
                    createResult.Errors.ToList().ForEach(e => ModelState.AddModelError(string.Empty, e.Description));
                    return Page();
                }
            }
            else
            {
                // Load the user 
                user = await _userManager.FindByNameAsync(username);
                if (user == null) return NotFound();
                if (user.UserName == User.Identity.Name) selfedit = true;
            }

            // Set email
            var muser = await _userManager.FindByNameAsync(Input.Email);
            if (muser != null && muser.UserName != user.UserName)
            {
                ModelState.AddModelError("Email", "Tato emailová adresa je již použita.");
               // Input = LoadInputModelById(id);
                return Page();
            }

            user.Email = Input.Email;
            user.NormalizedEmail = user.Email.ToUpper();
            user.UserName = Input.Email;
            user.NormalizedUserName = Input.Email;
            user.BirthDate = Input.BirthDate;
            user.GivenName = Input.GivenName;
            user.Surname = Input.Surname;

            await _dbctx.SaveChangesAsync();
            
            // Renew roles, but only if admin is signed in
            if (User.IsInRole(RolesMetadata.ADMIN))
            {
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
                await _userManager.AddToRolesAsync(user, Input.Roles);
            }

            // Reset password if desired.
            if (!string.IsNullOrEmpty(Input.Password))
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, Input.Password);
            }

            if (selfedit)
            {
                await _signInManager.RefreshSignInAsync(user);
            }

            return RedirectToPage("/Index");
        }
    }
}
