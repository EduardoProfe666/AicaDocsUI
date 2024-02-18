using AicaDocsUI.Extensions;
using AicaDocsUI.Models;
using AicaDocsUI.Repositories.Nomenclators;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Nomenclators;

public class Index : PageModel
{
    private readonly INomenclatorRepository _repository;
    private readonly INotyfService _toastNotificationService;
    public TypeOfNomenclator ValueNomenclator { get; set; } = TypeOfNomenclator.ProcessOfDocument;
    public IEnumerable<SelectListItem> ListItems { get; set; } = new List<SelectListItem>();

    public Index(INomenclatorRepository repository, INotyfService toastNotificationService)
    {
        _toastNotificationService = toastNotificationService;
        _toastNotificationService.Success("Nomenclador creado con éxito");
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

    }

    public void OnPostSuccessNotification()
    {
        var created1 = TempData["Created Nomenclator"] as bool? ?? false;
        var edited1 = TempData["Edited Nomenclator"] as bool? ?? false;
        
        _toastNotificationService.Success("Nomenclador creado con éxito");
        if (created1)
            _toastNotificationService.Success("Nomenclador creado con éxito");
        
        if(edited1)
            _toastNotificationService.Success("Nomenclador actualizado con éxito");
        
        TempData["Created Nomenclator"] = false;
        TempData["Edited Nomenclator"] = false;
    }
}