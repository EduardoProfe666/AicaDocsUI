using AicaDocsUI.Extensions;
using AicaDocsUI.Models;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Nomenclators;

public class Index : PageModel
{
    private readonly INomenclatorRepository _repository;
    public bool ShowCreated { get; set; }
    public bool ShowEdited { get; set; }
    public TypeOfNomenclator ValueNomenclator { get; set; } = TypeOfNomenclator.ProcessOfDocument;
    public IEnumerable<SelectListItem> ListItems { get; set; } = new List<SelectListItem>();

    public Index(INomenclatorRepository repository)
    {
        this._repository = repository;
    }

    public IEnumerable<Nomenclator> Nomenclators { get; set; } = new List<Nomenclator>();

    public async Task OnGetAsync(TypeOfNomenclator? valueNomenclator)
    {
        if (valueNomenclator != null)
            ValueNomenclator = valueNomenclator.Value;
        
        var data = await _repository.GetNomenclatorsByTypeAsync((int)ValueNomenclator);
        if (data != null)
            Nomenclators = data;
        ListItems = Enum.GetValues(typeof(TypeOfNomenclator))
            .Cast<TypeOfNomenclator>()
            .Select(v => new SelectListItem
            {
                Text = v.GetDescription(),
                Value = v.ToString()
            });
        
        ShowCreated = TempData["Created Nomenclator"] as bool? ?? false;
        ShowEdited = TempData["Edited Nomenclator"] as bool? ?? false;
        
        TempData["Created Nomenclator"] = false;
        TempData["Edited Nomenclator"] = false;

    }
    
}