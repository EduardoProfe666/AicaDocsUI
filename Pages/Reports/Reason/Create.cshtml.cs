using AicaDocsUI.Pages.PagesModelsData.Models.Nomenclators;
using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Reports.Reason;

public class Create : PageModel
{
    private readonly INomenclatorRepository _repository;

    [BindProperty] public NomenclatorCreatedModel NomenclatorModel
    { get; set; }

    public Create(INomenclatorRepository repository)
    {
        this._repository = repository;
    }

    public void OnGet()
    {
        NomenclatorModel = new() { Name = "", Type = TypeOfNomenclator.ReasonOfDownload };
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            NomenclatorModel.Type = TypeOfNomenclator.ReasonOfDownload;
            await _repository.CreateNomenclatorAsync(new NomenclatorCreatedDto(){Name = NomenclatorModel.Name, Type = NomenclatorModel.Type});
            TempData["Created Reason"] = true;
            Response.Redirect("/Reports/Reason/Index");

            //clear the Form
            NomenclatorModel.Name = "";
            NomenclatorModel.Type = TypeOfNomenclator.ReasonOfDownload;
            ModelState.Clear();
        }
    }
    
}