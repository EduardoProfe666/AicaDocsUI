using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

    public IEnumerable<NomenclatorDto> Nomenclators { get; set; } = new List<NomenclatorDto>();

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