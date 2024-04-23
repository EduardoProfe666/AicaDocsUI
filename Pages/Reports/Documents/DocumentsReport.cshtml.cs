using System.Net.Mime;
using AicaDocsUI.Extensions;
using AicaDocsUI.Repositories.ApiData.Dto.FilterCommons;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.ApiData.Dto.Users.Filter;
using AicaDocsUI.Repositories.Users;
using AicaDocsUI.Utils.RootProviderServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Reports.Documents;

public class DocumentsReport : PageModel
{
    private readonly IUserRepository _userRepository;
    private readonly RootProvider _rootProvider;

    public DocumentsReport(IUserRepository userRepository, RootProvider rootProvider)
    {
        _userRepository = userRepository;
        _rootProvider = rootProvider;
    }

    public IEnumerable<SelectListItem> ListEmail { get; set; } = new List<SelectListItem>();
    [BindProperty] public bool IsParam { get; set; }
    [BindProperty] public string Email { get; set; }

    
    
    public async Task OnGet()
    {
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
        
    }

    public IActionResult OnPost()
    {
        if (IsParam)
        {
            TempData["DownloadUrl"] = $"{_rootProvider.RootUI}/downloadReport/{DownloadReportEndpoint.TypeReport.DocumentsByUser}/0/{Email}";
        }
        else
        {
            TempData["DownloadUrl"] = $"{_rootProvider.RootUI}/downloadReport/{DownloadReportEndpoint.TypeReport.Documents}/1/1";
        }
        return RedirectToPage("/Reports/Documents/Index");
    }
}