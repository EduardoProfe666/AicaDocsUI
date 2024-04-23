using AicaDocsUI.Extensions;
using AicaDocsUI.Repositories.ApiData.Dto.FilterCommons;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.ApiData.Dto.Users;
using AicaDocsUI.Repositories.ApiData.Dto.Users.Filter;
using AicaDocsUI.Repositories.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Users;

public class Index : PageModel
{
    private readonly IUserRepository _userRepository;
    
    public bool ShowCreated { get; set; }
    public bool ShowDeleted { get; set; }
    public bool ShowUserExists { get; set; }
    
    public string DownloadUrl { get; set; }
    
    public int PageTotal { get; set; }

    public FilterUserDto Filter { get; set; }

    [BindProperty] public string? FullName { get; set; }
    [BindProperty] public string? Email { get; set; }
    [BindProperty] public UserRole? Role { get; set; }
    [BindProperty] public SortByUser SortBy { get; set; }
    [BindProperty] public SortOrder SortOrder { get; set; }
    [BindProperty] public int PageNumber { get; set; }
    
    public IEnumerable<SelectListItem> ListRoles { get; set; } = new List<SelectListItem>();

    public Index(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IEnumerable<UserDataDto> Users { get; set; } = new List<UserDataDto>();

    public async Task OnGetAsync(string? fullName, string? email, UserRole? role, SortOrder? sortOrder,
        SortByUser? sortBy, int? pageNumber)
    {
        Filter = new FilterUserDto()
        {
            Fullname = fullName,
            Email = email,
            Role = role,
            SortOrder = SortOrder.Asc,
            SortBy = SortByUser.Id,
            PaginationParams = new PaginationParams()
            {
                PageSize = 5,
                PageNumber = 1
            },
            NotIncludeThisAccount = true
        };

        if (sortBy != null)
            Filter.SortBy = sortBy.Value;
        if (sortOrder != null)
            Filter.SortOrder = sortOrder.Value;
        
        if (pageNumber != null)
            Filter.PaginationParams.PageNumber = pageNumber.Value;
        
        ListRoles = Enum.GetValues(typeof(UserRole))
            .Cast<UserRole>()
            .Select(v => new SelectListItem
            {
                Text = v.GetDescription(),
                Value = v.ToString()
            });


        var data1 = (await _userRepository.GetUserDataFilterAsync(Filter))!;
        Users = data1!.Response;
        PageTotal = data1.TotalPages;

        FullName = Filter.Fullname;
        Email = Filter.Email;
        Role = Filter.Role;
        SortBy = Filter.SortBy;
        SortOrder = Filter.SortOrder;
        PageNumber = Filter.PaginationParams.PageNumber;
        
        ShowCreated = TempData["Created User"] as bool? ?? false;
        ShowDeleted = TempData["Deleted User"] as bool? ?? false;
        ShowUserExists = TempData["User Exists"] as bool? ?? false;
        DownloadUrl = TempData["DownloadUrl"] as string ?? "";
        
        TempData["Created User"] = false;
        TempData["Deleted User"] = false;
        TempData["User Exists"] = false;
        TempData["DownloadUrl"] = "";
    }
    
    public async Task<IActionResult> OnGetDeleteAsync(string email)
    {
        var result = await _userRepository.DeleteUserAsync(email);
        if (result)
        {
            TempData["Deleted User"] = true;
        }

        return RedirectToPage();
    }
    
}