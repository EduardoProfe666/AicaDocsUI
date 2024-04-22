using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    public int Code {get; set;}

    public IActionResult OnGet(int? code)
    {
        Code = code ?? 500;
        HttpContext.Response.StatusCode = Code;

        if (Code == 401)
        {
            TempData["Unauthorized"] = true;
            return RedirectToPage("/Account/Login");
        }
            
        else if(Request.Headers["Referer"].ToString() == "/" && Code != 403 && Code != 401 && Code != 400)
            return RedirectToPage("/");
        return Page();
    }
}