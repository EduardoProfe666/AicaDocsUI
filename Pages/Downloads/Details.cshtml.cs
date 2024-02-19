using AicaDocsUI.Models;
using AicaDocsUI.Repositories.Documents;
using AicaDocsUI.Repositories.Downloads;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Downloads;

public class Details : PageModel
{
    private readonly IDownloadRepository _downloadRepository;
    private readonly INomenclatorRepository _nomenclatorRepository;
    private readonly IDocumentRepository _documentRepository;
    public Download Download { get; set; }
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
        Download = (await _downloadRepository.GetDownloadById(id))!;
        var data = await _nomenclatorRepository.GetNomenclatorAsync((int)TypeOfNomenclator.ReasonOfDownload,
            Download.ReasonId);
        Reason = data!.Name;

        var data1 = await _documentRepository.GetDocumentById(Download.DocumentId);
        CodeEdition = $"{data1!.Code}-{data1.Edition}";
    }
}