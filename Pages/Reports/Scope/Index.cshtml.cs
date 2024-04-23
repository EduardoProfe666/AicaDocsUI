using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Reports.Scope;

public class Index : PageModel
{
    private readonly INomenclatorRepository _repository;
    public bool ShowCreated { get; set; }
    public bool ShowEdited { get; set; }
    public bool ShowDeleted { get; set; }
    
    public bool ShowErrorUnique { get; set; }
    
    public bool ShowErrorDelete { get; set; }


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
        ShowDeleted = TempData["Deleted Process"] as bool? ?? false;
        ShowErrorDelete = TempData["Error to delete"] as bool? ?? false;
        ShowErrorUnique = TempData["Error Unique"] as bool? ?? false;
        
        TempData["Created Scope"] = false;
        TempData["Edited Scope"] = false;
        TempData["Deleted Process"] = false;
        TempData["Error to delete"] = false;
        TempData["Error Unique"] = false;

    }
    
    public async Task<IActionResult> OnGetDeleteAsync(int id)
    {
        var result = await _repository.DeleteNomenclatorAsync((int)TypeOfNomenclator.ScopeOfDocument, id);
        if (result)
        {
            TempData["Deleted Process"] = true;
        }
        else
        {
            TempData["Error to delete"] = true; 
        }

        return RedirectToPage();
    }
    
}