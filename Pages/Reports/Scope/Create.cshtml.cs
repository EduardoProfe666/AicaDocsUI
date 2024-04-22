using AicaDocsUI.Pages.PagesModelsData.Models.Nomenclators;
using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Reports.Scope;

public class Create : PageModel
{
    private readonly INomenclatorRepository _repository;

    [BindProperty] public NomenclatorCreatedModel NomenclatorModel { get; set; }

    public Create(INomenclatorRepository repository)
    {
        this._repository = repository;
    }

    public void OnGet()
    {
        NomenclatorModel = new() { Name = "", Type = TypeOfNomenclator.ScopeOfDocument };
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            NomenclatorModel.Type = TypeOfNomenclator.ScopeOfDocument;
            await _repository.CreateNomenclatorAsync(new NomenclatorCreatedDto(){Name = NomenclatorModel.Name, Type = NomenclatorModel.Type});
            TempData["Created Scope"] = true;
            Response.Redirect("/Reports/Scope/Index");

            //clear the Form
            NomenclatorModel.Name = "";
            NomenclatorModel.Type = TypeOfNomenclator.ScopeOfDocument;
            ModelState.Clear();
        }
    }
    
}