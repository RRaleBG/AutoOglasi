using AutoOglasi.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoOglasi.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly ILogger<LoginModel> _logger;
        public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public string ReturnUrl { get; set; } = string.Empty;

        public IList<AuthenticationScheme> ExternalLogins { get; set; } = new List<AuthenticationScheme>();

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ReturnUrl = NormalizeReturnUrl(returnUrl);
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            _logger.LogInformation("Login page opened. ReturnUrl: {ReturnUrl}", ReturnUrl);
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = NormalizeReturnUrl(returnUrl);
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (!ModelState.IsValid)
            {
                _logger.LogInformation("ModelState invalid on login: {Errors}", string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

            _logger.LogInformation("Login attempt for {Email}: Succeeded={Succeeded}, LockedOut={LockedOut}, RequiresTwoFactor={TwoFactor}", Input.Email, result.Succeeded, result.IsLockedOut, result.RequiresTwoFactor);

            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in: {Email}", Input.Email);
                return LocalRedirect(ReturnUrl);
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out: {Email}", Input.Email);
                return RedirectToPage("/Account/Lockout");
            }

            if (result.RequiresTwoFactor)
            {
                _logger.LogInformation("Two-factor required for: {Email}", Input.Email);
                ModelState.AddModelError(string.Empty, "Two-factor authentication is not configured for this application.");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            _logger.LogWarning("Invalid login attempt for {Email}", Input.Email);

            return Page();
        }

        private string NormalizeReturnUrl(string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return returnUrl;
            }

            var referer = Request?.Headers["Referer"].ToString();
            if (Uri.TryCreate(referer, UriKind.Absolute, out var refererUri))
            {
                var localReturnUrl = $"{refererUri.PathAndQuery}{refererUri.Fragment}";
                if (Url.IsLocalUrl(localReturnUrl)
                    && !localReturnUrl.Contains("/Identity/Account/Login", System.StringComparison.OrdinalIgnoreCase))
                {
                    return localReturnUrl;
                }
            }

            return Url.Content("~/");
        }
    }
}