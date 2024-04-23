using AicaDocsUI.Repositories.ApiData.Dto.Users;
using AicaDocsUI.Repositories.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Account.Manage;

public class Index : PageModel
{
    private readonly IAuthRepository _authRepository;

    public string? FullName { get; set; }
    
    public string? Email { get; set; }
    
    public bool ShowChangedPassword { get; set; }


    public Index(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<IActionResult> OnGet()
    {
        bool b = await _authRepository.IsLoginAdvanceAsync();

        if (!b)
        {
            TempData["Unauthorized"] = true;
            return RedirectToPage("/Account/Login");
        }
        
        var data = await _authRepository.GetInfoAsync();

        FullName = data?.FullName;
        Email = data?.Email;
        
        ShowChangedPassword = TempData["Changed Password"] as bool? ?? false;
        TempData["Changed Password"] = false;

        return Page();
    }
}