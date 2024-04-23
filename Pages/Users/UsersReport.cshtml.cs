using AicaDocsUI.Extensions;
using AicaDocsUI.Pages.Reports;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.Users;
using AicaDocsUI.Utils.RootProviderServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Users;

public class UsersReport : PageModel
{
    private readonly IUserRepository _userRepository;
    private readonly RootProvider _rootProvider;

    public UsersReport(IUserRepository userRepository, RootProvider rootProvider)
    {
        _userRepository = userRepository;
        _rootProvider = rootProvider;
    }

    public IEnumerable<SelectListItem> ListRoles { get; set; } = new List<SelectListItem>();
    [BindProperty] public bool IsParam { get; set; }
    [BindProperty] public UserRole Role { get; set; }

    
    
    public void OnGet()
    {
        IsParam = false;
        Role = UserRole.Admin;
        ListRoles = Enum.GetValues(typeof(UserRole))
            .Cast<UserRole>()
            .Select(v => new SelectListItem
            {
                Text = v.GetDescription(),
                Value = v.ToString()
            });
        
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