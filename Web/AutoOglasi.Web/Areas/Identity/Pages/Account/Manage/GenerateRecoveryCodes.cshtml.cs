using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using AutoOglasi.Data.Models;

namespace Microsoft.AspNetCore.Identity.UI.Pages.Account.Manage.Internal
{
    public class GenerateRecoveryCodesModel : PageModel
    {
        [TempData]
        public string StatusMessage { get; set; }

        public IEnumerable<string> RecoveryCodes { get; set; }

        public IActionResult OnGet()
        {
            if (TempData.TryGetValue("RecoveryCodes", out var obj) && obj is string codesStr)
            {
                RecoveryCodes = codesStr.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                RecoveryCodes = Array.Empty<string>();
            }

            return Page();
        }
    }
}
