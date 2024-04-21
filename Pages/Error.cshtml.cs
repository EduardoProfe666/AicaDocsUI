using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    public int Code {get; set;}

    public void OnGet(int? code)
    {
        Code = code ?? 500;
        if(Code == 401)
            Response.Redirect("/Account/Login");
    }
}