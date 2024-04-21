using AicaDocsUI.Pages.PagesModelsData.Models.Nomenclators;
using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Process;

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
        NomenclatorModel = new() { Name = "", Type = TypeOfNomenclator.ProcessOfDocument };
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            NomenclatorModel.Type = TypeOfNomenclator.ProcessOfDocument;
            await _repository.CreateNomenclatorAsync(new NomenclatorCreatedDto(){Name = NomenclatorModel.Name,Type = NomenclatorModel.Type});
            TempData["Created Process"] = true;
            Response.Redirect("/Process/Index");

            //clear the Form
            NomenclatorModel.Name = "";
            NomenclatorModel.Type = TypeOfNomenclator.ProcessOfDocument;
            ModelState.Clear();
        }
    }
    
}
