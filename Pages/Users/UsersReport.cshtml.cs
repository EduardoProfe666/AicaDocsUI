using AicaDocsUI.Extensions;
using AicaDocsUI.Pages.Reports;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Repositories.Users;
using AicaDocsUI.Utils.RootProviderServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Users;

public class UsersReport : PageModel
{
    private readonly IAuthRepository _authRepository;
    private readonly RootProvider _rootProvider;

    public UsersReport(RootProvider rootProvider, IAuthRepository authRepository)
    {
        _rootProvider = rootProvider;
        _authRepository = authRepository;
    }

    public IEnumerable<SelectListItem> ListRoles { get; set; } = new List<SelectListItem>();
    [BindProperty] public bool IsParam { get; set; }
    [BindProperty] public UserRole Role { get; set; }

    
    
    public async Task<IActionResult> OnGet()
    {
        bool b = await _authRepository.IsLoginAdvanceAsync();

        if (!b)
        {
            TempData["Unauthorized"] = true;
            return RedirectToPage("/Account/Login");
        }

        var c = await _authRepository.GetUserRoleAsync();
        if (c == null)
            return RedirectToPage();
        
        if (c != UserRole.Admin)
        {
            return RedirectToPage("/Error", new { code = 403 });
        }
        
        IsParam = false;
        Role = UserRole.Admin;
        ListRoles = Enum.GetValues(typeof(UserRole))
            .Cast<UserRole>()
            .Select(v => new SelectListItem
            {
                Text = v.GetDescription(),
                Value = v.ToString()
            });

        return Page();

    }

    public IActionResult OnPost()
    {
        if (IsParam)
        {
            TempData["DownloadUrl"] = $"{_rootProvider.RootUI}/downloadReport/{DownloadReportEndpoint.TypeReport.UsersRoles}/{(int) Role}/1";
        }
        else
        {
            TempData["DownloadUrl"] = $"{_rootProvider.RootUI}/downloadReport/{DownloadReportEndpoint.TypeReport.Users}/1/1";
        }
        return RedirectToPage("/Users/Index");
    }
    
}