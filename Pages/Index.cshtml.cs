using System.ComponentModel.DataAnnotations;
using AicaDocsUI.Repositories.ApiData.Dto.Auth;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Repositories.Reports;
using AicaDocsUI.Utils.RootProviderServices;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages;

public class IndexModel : PageModel 
{
    private readonly IAuthRepository _auth;
    private readonly RootProvider _rootProvider;

    public bool IsLogin { get; set; }
    public string? FullName { get; set; }

    public UserRole? UserRole { get; set; }
    
    public string AicaDocsApi { get; set; }

    public IndexModel(IAuthRepository auth, RootProvider rootProvider)
    {
        _auth = auth;
        _rootProvider = rootProvider;
        AicaDocsApi = _rootProvider.RootApi;
    }

    public async Task OnGet()
    { 
        //_auth.Logout();
        //var b = await _auth.LoginAdvance(new LoginRequestDto() { Email = "aicadocsworker@worker.cu", Password = "AicaDocs_Worker1!" });

        IsLogin = await _auth.IsLoginAdvance();

        if (IsLogin)
        {
            var data = await _auth.GetInfo();
            FullName = data!.FullName;
            UserRole = data.Role;
        }
        
    }
}