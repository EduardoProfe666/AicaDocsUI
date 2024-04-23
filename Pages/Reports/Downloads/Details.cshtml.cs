using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Downloads;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Repositories.Documents;
using AicaDocsUI.Repositories.Downloads;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Reports.Downloads;

public class Details : PageModel
{
    private readonly IDownloadRepository _downloadRepository;
    private readonly INomenclatorRepository _nomenclatorRepository;
    private readonly IDocumentRepository _documentRepository;
    private readonly IAuthRepository _authRepository;
    public DownloadDto DownloadDto { get; set; }
    public string Reason { get; set; }
    public string CodeEdition { get; set; }

    public Details(IDownloadRepository downloadRepository, INomenclatorRepository nomenclatorRepository,
        IDocumentRepository documentRepository, IAuthRepository authRepository)
    {
        _downloadRepository = downloadRepository;
        _nomenclatorRepository = nomenclatorRepository;
        _documentRepository = documentRepository;
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
        
        if (c != UserRole.Admin)
        {
            return RedirectToPage("/Error", new { code = 403 });
        }
        
        DownloadDto = (await _downloadRepository.GetDownloadByIdAsync(id))!;
        var data = await _nomenclatorRepository.GetNomenclatorAsync((int)TypeOfNomenclator.ReasonOfDownload,
            DownloadDto.ReasonId);
        Reason = data!.Name;

        var data1 = await _documentRepository.GetDocumentByIdAsync(DownloadDto.DocumentId);
        CodeEdition = $"{data1!.Code}-{data1.Edition}";

        return Page();
    }
}