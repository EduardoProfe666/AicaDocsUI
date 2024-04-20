using AicaDocsUI.Extensions;
using AicaDocsUI.Models;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Type;

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
        
        var data = await _repository.GetNomenclatorsByTypeAsync((int)TypeOfNomenclator.TypeOfDocument);
        if (data != null)
            Nomenclators = data;
        
        ShowCreated = TempData["Created Type"] as bool? ?? false;
        ShowEdited = TempData["Edited Type"] as bool? ?? false;
        
        TempData["Created Type"] = false;
        TempData["Edited Type"] = false;

    }
    
}