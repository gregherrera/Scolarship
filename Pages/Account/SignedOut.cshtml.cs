using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Escolaridad.Data;

public class SignedOutModel : PageModel
{

    public IActionResult OnGet()
    {
        if (User.Identity.IsAuthenticated)
        {
            // Redirect to home page if the user is authenticated.
            return RedirectToPage("/Index");
        }

        return Page();
    }
}