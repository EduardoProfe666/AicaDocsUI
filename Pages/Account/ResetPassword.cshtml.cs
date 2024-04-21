using AicaDocsUI.Pages.PagesModelsData.Models.Auth;
using AicaDocsUI.Repositories.Auth;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AicaDocsUI.Pages.Account;

public class ResetPassword : PageModel
{
    private readonly IAuthRepository _authRepository;

    [BindProperty] public ResetPasswordRequestModel ResetPasswordRequestModel { get; set; }

    [BindProperty] public WrapperInfoResetPassword WrapperInfo { get; set; }

    public ResetPassword(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public IActionResult OnGet(string? email, string? fullName, string? changePasswordToken)
    {
        if (email == null || fullName == null || changePasswordToken == null)
            return RedirectToPage("/Error", new { code = 400 });
        
        WrapperInfo = new WrapperInfoResetPassword() { Email = email, FullName = fullName, ChangePasswordToken = changePasswordToken};
            
        ResetPasswordRequestModel = new ResetPasswordRequestModel()
        {
            NewPassword = "",
            RepeatNewPassword = ""
        };
        return Page();
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var b = await _authRepository.ResetPasswordAsync(new ResetPasswordRequest
            {
                Email = WrapperInfo.Email,
                NewPassword = ResetPasswordRequestModel.NewPassword,
                ResetCode = WrapperInfo.ChangePasswordToken
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
                TempData["New Password"] = true;
            }
            else
            {
                TempData["Index Error"] = true;
                Response.Redirect("/");
            }
        }
    }

    public class WrapperInfoResetPassword()
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string ChangePasswordToken { get; set; }
    }
}