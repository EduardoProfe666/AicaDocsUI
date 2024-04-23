using System.ComponentModel.DataAnnotations;
using AicaDocsUI.Repositories.ApiData.Dto.Auth;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Repositories.Reports;
using AicaDocsUI.Utils.RootProviderServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages;

public class IndexModel : PageModel 
{
    private readonly IAuthRepository _auth;
    private readonly RootProvider _rootProvider;
    
    public bool ShowForgotPassword { get; set; }
    public bool ShowError { get; set; }

    public bool IsLogin { get; set; }
    public string? FullName { get; set; }

    public UserRole? UserRole { get; set; }
    
    public string AicaDocsApi { get; set; }

    public IndexModel(IAuthRepository auth, RootProvider rootProvider)
    {
        _auth = auth;
        _rootProvider = rootProvider;
        AicaDocsApi = _rootProvider.RootApi;
    }

    public async Task<IActionResult> OnGet()
    { 
        IsLogin = await _auth.IsLoginAdvanceAsync();

        if (IsLogin)
        {
            var data = await _auth.GetInfoAsync();
            if (data is null)
                return RedirectToPage();
            FullName = data?.FullName;
            UserRole = data?.Role;
        }
        
        ShowForgotPassword = TempData["Forgot Password"] as bool? ?? false;
        TempData["Forgot Password"] = false;
        
        ShowError = TempData["Index Error"] as bool? ?? false;
        TempData["Index Error"] = false;
        
        return Page();
    }
}