using AicaDocsUI.Pages.PagesModelsData.Models.Auth;
using AicaDocsUI.Repositories.ApiData.Dto.Auth;
using AicaDocsUI.Repositories.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Account;

public class ForgotPassword : PageModel
{
    private readonly IAuthRepository _authRepository;
    
    [BindProperty] public ForgotPasswordModel ForgotPasswordModel { get; set; }

    public ForgotPassword(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task OnGet()
    {
        if (await _authRepository.IsLoginAdvanceAsync())
            Response.Redirect("/");
        else
        {
            ForgotPasswordModel = new ForgotPasswordModel() { Email = ""};
        }
        
    }
    
    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var b = await _authRepository.ForgotPasswordAsync(ForgotPasswordModel.Email);
            
            if (b)
            {
                Response.Redirect("/");
                //clear the Form
                ForgotPasswordModel.Email = "";
                ModelState.Clear();
                TempData["Forgot Password"] = true;
            }
            else
            {
                TempData["Index Error"] = true;
                Response.Redirect("/");
            }
            
        }
    }
}