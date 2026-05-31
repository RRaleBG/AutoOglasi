
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microsoft.AspNetCore.Identity.UI.Pages.Account.Manage.Internal
{
    public class IndexModel : PageModel
    {
        // Ova klasa ostaje prazna jer će Azure u pozadini 
        // povući ugrađenu logiku, ali nam je potreban ovaj fajl
        // kako bi Razor kompajler uspešno povezao @model direktivu.
    }
}
