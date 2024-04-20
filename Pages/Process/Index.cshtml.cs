using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Process;

public class Index : PageModel
{
    private readonly INomenclatorRepository _repository;
    public bool ShowCreated { get; set; }
    public bool ShowEdited { get; set; }

    public bool ShowDeleted { get; set; }
    
    public bool ShowErrorDelete { get; set; }


    public Index(INomenclatorRepository repository)
    {
        this._repository = repository;
    }

    public IEnumerable<NomenclatorDto> Nomenclators { get; set; } = new List<NomenclatorDto>();

    public async Task OnGetAsync()
    {
        var data = await _repository.GetNomenclatorsByTypeAsync((int)TypeOfNomenclator.ProcessOfDocument);
        if (data != null)
            Nomenclators = data;

        ShowCreated = TempData["Created Process"] as bool? ?? false;
        ShowEdited = TempData["Edited Process"] as bool? ?? false;
        ShowDeleted = TempData["Deleted Process"] as bool? ?? false;
        ShowErrorDelete = TempData["Error to delete"] as bool? ?? false;

        TempData["Created Process"] = false;
        TempData["Edited Process"] = false;
        TempData["Deleted Process"] = false;
        TempData["Error to delete"] = false;
    }

    public async Task<IActionResult> OnGetDeleteAsync(int id)
    {
        var result = await _repository.DeleteNomenclatorAsync((int)TypeOfNomenclator.ProcessOfDocument, id);
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