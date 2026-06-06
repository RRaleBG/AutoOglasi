
using AutoOglasi.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SD = System.Drawing;
using SD2 = System.Drawing.Drawing2D;
using SDI = System.Drawing.Imaging;

namespace Microsoft.AspNetCore.Identity.UI.Pages.Account.Manage.Internal
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<IndexModel> _logger;
        private readonly IWebHostEnvironment _environment;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<IndexModel> logger,
            IWebHostEnvironment environment)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        [BindProperty]
        public bool RemoveAvatar { get; set; }

        // Expose current profile image path for the view to render
        public string CurrentProfileImagePath { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Contact Phone Line")]
            public string PhoneNumber { get; set; } = string.Empty;

            [DataType(DataType.Upload)]
            public IFormFile ProfileImage { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Unable to load user.");
            }

            Input.PhoneNumber = await _userManager.GetPhoneNumberAsync(user) ?? string.Empty;
            CurrentProfileImagePath = user.ProfileImagePath;

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

            var currentPhone = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != currentPhone)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    foreach (var error in setPhoneResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
            }

            // Handle Remove Avatar request first
            if (RemoveAvatar)
            {
                var profilesPath = Path.Combine(_environment.WebRootPath, "images", "profiles");
                var files = Directory.GetFiles(profilesPath, user.Id + ".*");
                foreach (var f in files)
                {
                    try { System.IO.File.Delete(f); } catch { /* ignore */ }
                }

                user.ProfileImagePath = null;
                var updateResultRemove = await _userManager.UpdateAsync(user);
                if (!updateResultRemove.Succeeded)
                {
                    foreach (var e in updateResultRemove.Errors)
                    {
                        ModelState.AddModelError(string.Empty, e.Description);
                    }
                    return Page();
                }
            }

            // Handle profile image upload
            if (Input.ProfileImage != null && Input.ProfileImage.Length > 0)
            {
                var allowedExt = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var ext = Path.GetExtension(Input.ProfileImage.FileName).ToLowerInvariant();

                if (!allowedExt.Contains(ext))
                {
                    ModelState.AddModelError("Input.ProfileImage", "Unsupported image format. Allowed: jpg, jpeg, png, gif.");
                    return Page();
                }

                if (Input.ProfileImage.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("Input.ProfileImage", "Image size must be 2MB or less.");
                    return Page();
                }

                var profilesPath = Path.Combine(_environment.WebRootPath, "images", "profiles");
                Directory.CreateDirectory(profilesPath);

                var fileName = user.Id + ext;
                var physicalPath = Path.Combine(profilesPath, fileName);

                // Save original upload temporarily
                await using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    await Input.ProfileImage.CopyToAsync(stream);
                }

                // Resize to thumbnail (e.g., 128x128)
                try
                {
                    using var img = SD.Image.FromFile(physicalPath);
                    using var thumb = ResizeImage(img, 128, 128);
                    // Always save as jpeg to ensure consistent format
                    thumb.Save(physicalPath, SDI.ImageFormat.Jpeg);
                }
                catch
                {
                    // If resizing failed, continue with the original file
                }

                user.ProfileImagePath = $"/images/profiles/{fileName}";

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    foreach (var e in updateResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, e.Description);
                    }
                    return Page();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User updated their profile information.");
            StatusMessage = "Your profile has been updated";

            return RedirectToPage();
        }
        private SD.Image ResizeImage(SD.Image image, int width, int height)
        {
            var destRect = new SD.Rectangle(0, 0, width, height);
            var destImage = new SD.Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = SD.Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = SD2.CompositingMode.SourceCopy;
                graphics.CompositingQuality = SD2.CompositingQuality.HighQuality;
                graphics.InterpolationMode = SD2.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SD2.SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = SD2.PixelOffsetMode.HighQuality;

                using var wrapMode = new SDI.ImageAttributes();
                wrapMode.SetWrapMode(SD2.WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, SD.GraphicsUnit.Pixel, wrapMode);
            }

            return destImage;
        }
    }
}
