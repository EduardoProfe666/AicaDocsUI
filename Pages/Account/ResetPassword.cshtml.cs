using AicaDocsUI.Pages.PagesModelsData.Models.Auth;
using AicaDocsUI.Repositories.Auth;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Account;

public class ResetPassword : PageModel
{
    private readonly IAuthRepository _authRepository;

    [BindProperty] public ResetPasswordRequestModel ResetPasswordRequestModel { get; set; }

    private string Email { get; set; }
    public string FullName { get; set; }
    private string ChangePasswordToken { get; set; }

    public ResetPassword(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public void OnGet(string? email, string? fullName, string? changePasswordToken)
    {
        if (email is null || fullName is null || changePasswordToken is null)
            Response.Redirect("/Error?code=400");
        else
        {
            Email = email;
            FullName = fullName;
            ChangePasswordToken = changePasswordToken;
            
            ResetPasswordRequestModel = new ResetPasswordRequestModel()
            {
                NewPassword = "",
                RepeatNewPassword = ""
            };
        }
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var b = await _authRepository.ResetPasswordAsync(new ResetPasswordRequest
            {
                Email = Email,
                NewPassword = ResetPasswordRequestModel.NewPassword,
                ResetCode = ChangePasswordToken
            });

            if (b)
            {
                if (await _authRepository.IsLoginAdvanceAsync())
                    _authRepository.Logout();
                Response.Redirect("/Account/Login");
                //clear the Form
                ResetPasswordRequestModel.NewPassword = "";
                ResetPasswordRequestModel.RepeatNewPassword = "";
                ModelState.Clear();
                TempData["Reset Password"] = true;
            }
            else
            {
                TempData["Index Error"] = true;
                Response.Redirect("/");
            }
        }
    }
}