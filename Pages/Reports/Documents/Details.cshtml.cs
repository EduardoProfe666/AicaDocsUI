using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Documents;
using AicaDocsUI.Repositories.Documents;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Reports.Documents;

public class Details : PageModel
{
    private readonly IDocumentRepository _documentRepository;
    private readonly INomenclatorRepository _nomenclatorRepository;
    public DocumentDto DocumentDto { get; set; }
    public string Type { get; set; }
    public string Process { get; set; }
    public string Scope { get; set; }

    public Details(IDocumentRepository documentRepository, INomenclatorRepository nomenclatorRepository)
    {
        _documentRepository = documentRepository;
        _nomenclatorRepository = nomenclatorRepository;
    }

    public async Task OnGetAsync(int id)
    {
        DocumentDto = (await _documentRepository.GetDocumentByIdAsync(id))!;
        
        var data = await _nomenclatorRepository.GetNomenclatorAsync((int)TypeOfNomenclator.TypeOfDocument, DocumentDto.TypeId);
        Type=data!.Name;
        
        var data2 = await _nomenclatorRepository.GetNomenclatorAsync((int)TypeOfNomenclator.ProcessOfDocument, DocumentDto.ProcessId);
        Process=data2!.Name;
        
        var data3 = await _nomenclatorRepository.GetNomenclatorAsync((int)TypeOfNomenclator.ScopeOfDocument, DocumentDto.ScopeId);
        Scope=data3!.Name;
    }
}