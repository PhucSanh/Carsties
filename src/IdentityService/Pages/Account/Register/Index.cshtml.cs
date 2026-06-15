using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Duende.IdentityServer;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using IdentityService.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Pages.Account.Register
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [BindProperty]
        public RegisterViewModel Input { get; set; }
        [BindProperty]
        public bool IsSuccess { get; set; }

        public IActionResult OnGet(string returnUrl)
        {
            Input = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };

            return Page();

        }

        public async Task<IActionResult> OnPost()
        {
             if(Input.Button == "cancel")
             {
                 return Redirect("~/");
             }
             if(ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.UserName,
                    Email = Input.Email,
                    EmailConfirmed = true,
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
               
                if (result.Succeeded) { 
                
                   await _userManager.AddClaimsAsync(user, new[]
                    {
                        new System.Security.Claims.Claim("name", Input.FullName)
                        
                    });
                   
                    IsSuccess = true;
                
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
             return Page();
        }
    }
}
