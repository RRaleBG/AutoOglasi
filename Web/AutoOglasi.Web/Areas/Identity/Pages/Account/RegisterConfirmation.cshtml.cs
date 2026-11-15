namespace AutoOglasi.Web.Areas.Identity.Pages.Account;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class RegisterConfirmationModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string Email { get; set; }

    public void OnGet(string email = null)
    {
        this.Email = email;
    }
}
