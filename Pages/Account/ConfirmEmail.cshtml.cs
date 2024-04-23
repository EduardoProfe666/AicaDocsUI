using AicaDocsUI.Pages.PagesModelsData.Models.Auth;
using AicaDocsUI.Repositories.ApiData.Dto.Auth;
using AicaDocsUI.Repositories.Auth;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Account;

public class ConfirmEmail : PageModel
{
    private readonly IAuthRepository _authRepository;

    [BindProperty] public ConfirmEmailModel ConfirmEmailModel { get; set; }

    [BindProperty] public WrapperInfoConfirmEmail WrapperInfo { get; set; }

    public ConfirmEmail(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public IActionResult OnGet(string? email, string? fullName, string? changePasswordToken, string? confirmEmailToken)
    {
        if (email is null || fullName is null || changePasswordToken is null || confirmEmailToken is null)
            return RedirectToPage("/Error", new { code = 400 });
        WrapperInfo = new WrapperInfoConfirmEmail()
        {
            Email = email, FullName = fullName, ChangePasswordToken = changePasswordToken,
            ConfirmEmailToken = confirmEmailToken
        };

        ConfirmEmailModel = new ConfirmEmailModel()
        {
            Password = "",
            RepeatNewPassword = ""
        };
        return Page();
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var b = await _authRepository.ConfirmEmailAsync(new ConfirmEmailDto()
            {
                Email = WrapperInfo.Email,
                Password = ConfirmEmailModel.Password,
                ChangePasswordToken = WrapperInfo.ChangePasswordToken,
                ConfirmEmailToken = WrapperInfo.ConfirmEmailToken
            });

            if (b)
            {
                if (await _authRepository.IsLoginAdvanceAsync())
                    _authRepository.Logout();
                Response.Redirect("/Account/Login");
                //clear the Form
                ConfirmEmailModel.Password = "";
                ConfirmEmailModel.RepeatNewPassword = "";
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

    public class WrapperInfoConfirmEmail()
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string ChangePasswordToken { get; set; }
        public string ConfirmEmailToken { get; set; }
    }
}