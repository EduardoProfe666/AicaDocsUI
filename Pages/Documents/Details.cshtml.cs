using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Documents;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Repositories.Documents;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Documents;

public class Details : PageModel
{
    private readonly IDocumentRepository _documentRepository;
    private readonly INomenclatorRepository _nomenclatorRepository;
    private readonly IAuthRepository _authRepository;
    public DocumentDto DocumentDto { get; set; }
    public string Type { get; set; }
    public string Process { get; set; }
    public string Scope { get; set; }

    public Details(IDocumentRepository documentRepository, INomenclatorRepository nomenclatorRepository, IAuthRepository authRepository)
    {
        _documentRepository = documentRepository;
        _nomenclatorRepository = nomenclatorRepository;
        _authRepository = authRepository;
    }

    public async Task<IActionResult> OnGetAsync(int id)
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
        
        if (c != UserRole.Worker)
        {
            return RedirectToPage("/Error", new { code = 403 });
        }
        
        DocumentDto = (await _documentRepository.GetDocumentByIdAsync(id))!;
        
        var data = await _nomenclatorRepository.GetNomenclatorAsync((int)TypeOfNomenclator.TypeOfDocument, DocumentDto.TypeId);
        Type=data!.Name;
        
        var data2 = await _nomenclatorRepository.GetNomenclatorAsync((int)TypeOfNomenclator.ProcessOfDocument, DocumentDto.ProcessId);
        Process=data2!.Name;
        
        var data3 = await _nomenclatorRepository.GetNomenclatorAsync((int)TypeOfNomenclator.ScopeOfDocument, DocumentDto.ScopeId);
        Scope=data3!.Name;

        return Page();
    }
}