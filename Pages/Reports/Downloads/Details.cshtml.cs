using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Downloads;
using AicaDocsUI.Repositories.Documents;
using AicaDocsUI.Repositories.Downloads;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Reports.Downloads;

public class Details : PageModel
{
    private readonly IDownloadRepository _downloadRepository;
    private readonly INomenclatorRepository _nomenclatorRepository;
    private readonly IDocumentRepository _documentRepository;
    public DownloadDto DownloadDto { get; set; }
    public string Reason { get; set; }
    public string CodeEdition { get; set; }

    public Details(IDownloadRepository downloadRepository, INomenclatorRepository nomenclatorRepository,
        IDocumentRepository documentRepository)
    {
        _downloadRepository = downloadRepository;
        _nomenclatorRepository = nomenclatorRepository;
        _documentRepository = documentRepository;
    }

    public async Task OnGetAsync(int id)
    {
        DownloadDto = (await _downloadRepository.GetDownloadByIdAsync(id))!;
        var data = await _nomenclatorRepository.GetNomenclatorAsync((int)TypeOfNomenclator.ReasonOfDownload,
            DownloadDto.ReasonId);
        Reason = data!.Name;

        var data1 = await _documentRepository.GetDocumentByIdAsync(DownloadDto.DocumentId);
        CodeEdition = $"{data1!.Code}-{data1.Edition}";
    }
}