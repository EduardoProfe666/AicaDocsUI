using AicaDocsUI.Models;
using AicaDocsUI.Repositories.Downloads;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Downloads;

public class Details : PageModel
{
    private readonly IDownloadRepository _downloadRepository;
    private readonly INomenclatorRepository _nomenclatorRepository;
    public Download Download { get; set; }
    public string Reason { get; set; }

    public Details(IDownloadRepository downloadRepository, INomenclatorRepository nomenclatorRepository)
    {
        _downloadRepository = downloadRepository;
        _nomenclatorRepository = nomenclatorRepository;
    }

    public async Task OnGetAsync(int id)
    {
        Download = (await _downloadRepository.GetDownloadById(id))!;
        var data = await _nomenclatorRepository.GetNomenclatorAsync((int)TypeOfNomenclator.ReasonOfDownload, Download.ReasonId);
        Reason=data!.Name;
    }
}