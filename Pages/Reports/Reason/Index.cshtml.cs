using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Reports.Reason;

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
        
        var data = await _repository.GetNomenclatorsByTypeAsync((int)TypeOfNomenclator.ReasonOfDownload);
        if (data != null)
            Nomenclators = data;
        
        ShowCreated = TempData["Created Reason"] as bool? ?? false;
        ShowEdited = TempData["Edited Reason"] as bool? ?? false;
        ShowDeleted = TempData["Deleted Process"] as bool? ?? false;
        ShowErrorDelete = TempData["Error to delete"] as bool? ?? false;
        ShowErrorUnique = TempData["Error Unique"] as bool? ?? false;
        
        TempData["Created Reason"] = false;
        TempData["Edited Reason"] = false;
        TempData["Deleted Process"] = false;
        TempData["Error to delete"] = false;
        TempData["Error Unique"] = false;

    }
    
    public async Task<IActionResult> OnGetDeleteAsync(int id)
    {
        var result = await _repository.DeleteNomenclatorAsync((int)TypeOfNomenclator.ReasonOfDownload, id);
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