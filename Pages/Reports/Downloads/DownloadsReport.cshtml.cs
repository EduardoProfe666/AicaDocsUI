using AicaDocsUI.Repositories.ApiData.Dto.FilterCommons;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.ApiData.Dto.Users.Filter;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Repositories.Users;
using AicaDocsUI.Utils.RootProviderServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Reports.Downloads;

public class DownloadsReport : PageModel
{
    private readonly IUserRepository _userRepository;
    private readonly RootProvider _rootProvider;
    private readonly IAuthRepository _authRepository;

    public DownloadsReport(IUserRepository userRepository, RootProvider rootProvider, IAuthRepository authRepository)
    {
        _userRepository = userRepository;
        _rootProvider = rootProvider;
        _authRepository = authRepository;
    }

    public IEnumerable<SelectListItem> ListEmail { get; set; } = new List<SelectListItem>();
    [BindProperty] public bool IsParam { get; set; }
    [BindProperty] public string Email { get; set; }

    
    
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
        
        var data = await _userRepository.GetUserDataFilterAsync(new FilterUserDto()
        {
            Email = null,
            Fullname = null,
            NotIncludeThisAccount = false,
            Role = null,
            SortOrder = SortOrder.Asc,
            SortBy = SortByUser.Email,
            PaginationParams = new PaginationParams()
            {
                PageNumber = 1,
                PageSize = 1000
            } });
        var users = data.Response;
        ListEmail = users.Select(a => new SelectListItem()
        {
            Text = a.Email,
            Value = a.Email
        });
        
        IsParam = false;
        Email = users.ToList()[0].Email;

        return Page();
    }

    public IActionResult OnPost()
    {
        if (IsParam)
        {
            TempData["DownloadUrl"] = $"{_rootProvider.RootUI}/downloadReport/{DownloadReportEndpoint.TypeReport.DownloadsByUser}/0/{Email}";
        }
        else
        {
            TempData["DownloadUrl"] = $"{_rootProvider.RootUI}/downloadReport/{DownloadReportEndpoint.TypeReport.Downloads}/1/1";
        }
        return RedirectToPage("/Reports/Downloads/Index");
    }
}