using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using AutoOglasi.Data.Models;

namespace Microsoft.AspNetCore.Identity.UI.Pages.Account.Manage.Internal
{
    public class EnableAuthenticatorModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EnableAuthenticatorModel> _logger;

        public EnableAuthenticatorModel(UserManager<ApplicationUser> userManager, ILogger<EnableAuthenticatorModel> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [TempData]
        public string StatusMessage { get; set; }

        public string SharedKey { get; set; }
        public string AuthenticatorUri { get; set; }
        public string AuthenticatorUriEncoded { get; private set; }
        public string QrCodeImageUrl { get; set; }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        {
            [Required]
            [StringLength(7, MinimumLength = 6)]
            [DataType(DataType.Text)]
            [Display(Name = "Verification code")]
            public string Code { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Unable to load user.");
            }

            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            SharedKey = FormatKey(unformattedKey);
            AuthenticatorUri = GenerateQrCodeUri(user.Email, unformattedKey) ?? string.Empty;
            AuthenticatorUri = AuthenticatorUri.Trim();
            // Encoded form for embedding in QR provider query
            AuthenticatorUriEncoded = Uri.EscapeDataString(AuthenticatorUri);

            // Generate inline QR code image (data URI) using QRCoder
            if (!string.IsNullOrEmpty(AuthenticatorUri))
            {
                try
                {
                    using var qrGenerator = new QRCoder.QRCodeGenerator();
                    using var qrData = qrGenerator.CreateQrCode(AuthenticatorUri, QRCoder.QRCodeGenerator.ECCLevel.Q);
                    var png = new QRCoder.PngByteQRCode(qrData);
                    var qrBytes = png.GetGraphic(20);
                    QrCodeImageUrl = $"data:image/png;base64,{Convert.ToBase64String(qrBytes)}";
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to generate QR code image server-side.");
                    QrCodeImageUrl = null;
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Unable to load user.");
            }

            // Remove spaces and hypens
            var verificationCode = Input.Code.Replace(" ", string.Empty).Replace("-", string.Empty);
            var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

            if (!is2faTokenValid)
            {
                ModelState.AddModelError("Input.Code", "Verification code is invalid.");
                return Page();
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);
            _logger.LogInformation("User has enabled 2FA using an authenticator app.");

            // Generate recovery codes
            var codes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
            TempData["RecoveryCodes"] = string.Join("\n", codes);

            return RedirectToPage("./GenerateRecoveryCodes");
        }

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(' ');
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            // Format: otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6
            return string.Format(
                "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6",
                UrlEncoder.Default.Encode("AutoOglasi"),
                UrlEncoder.Default.Encode(email),
                unformattedKey);
        }
    }
}
