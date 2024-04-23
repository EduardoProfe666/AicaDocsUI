using AicaDocsUI.Pages.PagesModelsData.Models.Auth;
using AicaDocsUI.Repositories.ApiData.Dto.Auth;
using AicaDocsUI.Repositories.ApiData.Dto.Users;
using AicaDocsUI.Repositories.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Account.Manage;

public class ChangePassword : PageModel
{
    private readonly IAuthRepository _authRepository;

    public ChangePassword(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    [BindProperty] public ChangePasswordModel ChangePasswordModel { get; set; }

    public bool ShowError { get; set; }

    public void OnGetAsync()
    {
        ChangePasswordModel = new ChangePasswordModel() { NewPassword = "", OldPassword = "", RepeatNewPassword = "" };
        ShowError = TempData["Error Password"] as bool? ?? false;

        TempData["Error Password"] = false;
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var b = await _authRepository.ChangePasswordAsync(new ChangePasswordDto()
                { NewPassword = ChangePasswordModel.NewPassword, OldPassword = ChangePasswordModel.OldPassword });
            
            if (b)
            {
                Response.Redirect("/Account/Manage");
                //clear the Form
                ChangePasswordModel.RepeatNewPassword = "";
                ChangePasswordModel.OldPassword = "";
                ChangePasswordModel.NewPassword = "";
                ModelState.Clear();
                TempData["Changed Password"] = true;
            }
            else
            {
                TempData["Error Password"] = true;
                Response.Redirect("/Account/Manage/ChangePassword");
            }
        }
    }
}