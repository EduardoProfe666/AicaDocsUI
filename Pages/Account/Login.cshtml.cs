using AicaDocsUI.Pages.PagesModelsData.Models.Auth;
using AicaDocsUI.Repositories.ApiData.Dto.Auth;
using AicaDocsUI.Repositories.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Account;

public class Login : PageModel
{
    private readonly IAuthRepository _authRepository;
    
    public bool ShowErrorLogin { get; set; }
    
    [BindProperty] public LoginRequestModel LoginRequest { get; set; }

    public Login(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task OnGet()
    {
        if (await _authRepository.IsLoginAdvanceAsync())
            Response.Redirect("/");
        else
        {
            LoginRequest = new LoginRequestModel() { Email = "", Password = "" };
            ShowErrorLogin = TempData["Error Login"] as bool? ?? false;
            
            TempData["Error Login"] = false;
        }
        
    }
    
    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var b = await _authRepository.LoginAdvanceAsync(new LoginRequestDto(){Email = LoginRequest.Email, Password = LoginRequest.Password});
            
            if (b)
            {
                Response.Redirect("/");
                //clear the Form
                LoginRequest.Password = "";
                LoginRequest.Email = "";
                ModelState.Clear();
            }
            else
            {
                TempData["Error Login"] = true;
                Response.Redirect("/Account/Login");
            }
            
        }
    }
}