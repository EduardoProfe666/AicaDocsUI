using AicaDocsUI.Extensions;
using AicaDocsUI.Models;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Scope;

public class Index : PageModel
{
    private readonly INomenclatorRepository _repository;
    public bool ShowCreated { get; set; }
    public bool ShowEdited { get; set; }

    public Index(INomenclatorRepository repository)
    {
        this._repository = repository;
    }

    public IEnumerable<Nomenclator> Nomenclators { get; set; } = new List<Nomenclator>();

    public async Task OnGetAsync()
    {
        
        var data = await _repository.GetNomenclatorsByTypeAsync((int)TypeOfNomenclator.ScopeOfDocument);
        if (data != null)
            Nomenclators = data;
        
        ShowCreated = TempData["Created Scope"] as bool? ?? false;
        ShowEdited = TempData["Edited Scope"] as bool? ?? false;
        
        TempData["Created Scope"] = false;
        TempData["Edited Scope"] = false;

    }
    
}