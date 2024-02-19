using AicaDocsUI.Models;
using AicaDocsUI.Repositories.Documents;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Documents;

public class Details : PageModel
{
    private readonly IDocumentRepository _documentRepository;
    private readonly INomenclatorRepository _nomenclatorRepository;
    public Document Document { get; set; }
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
        Document = (await _documentRepository.GetDocumentById(id))!;
        
        var data = await _nomenclatorRepository.GetNomenclatorAsync((int)TypeOfNomenclator.TypeOfDocument, Document.TypeId);
        Type=data!.Name;
        
        var data2 = await _nomenclatorRepository.GetNomenclatorAsync((int)TypeOfNomenclator.ProcessOfDocument, Document.ProcessId);
        Process=data2!.Name;
        
        var data3 = await _nomenclatorRepository.GetNomenclatorAsync((int)TypeOfNomenclator.ScopeOfDocument, Document.ScopeId);
        Scope=data3!.Name;
    }
}