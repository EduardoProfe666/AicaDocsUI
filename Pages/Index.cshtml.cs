using AicaDocsUI.Repositories.ApiData.Dto.Auth;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Repositories.Reports;
using AicaDocsUI.Utils.RootProviderServices;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages;

public class IndexModel : PageModel
{
    private readonly IAuthRepository _auth;
    private readonly IReportRepository _docs;
    private readonly RootProvider _rootProvider;

    public string AicaDocsApi { get; set; }

    public IndexModel(IAuthRepository auth, IReportRepository docs, RootProvider rootProvider)
    {
        _auth = auth;
        _docs = docs;
        _rootProvider = rootProvider;
        AicaDocsApi = _rootProvider.RootApi;
    }

    public async Task OnGet()
    {
        var b = await _auth.LoginAdvance(new LoginRequestDto() { Email = "aicadocsadmin@admin.cu", Password = "AicaDocs_Admin1!" });

        Console.WriteLine(b);
    }
}