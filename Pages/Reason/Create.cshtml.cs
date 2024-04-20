using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Reason;

public class Create : PageModel
{
    private readonly INomenclatorRepository _repository;

    [BindProperty] public NomenclatorCreatedDto NomenclatorDto { get; set; }

    public Create(INomenclatorRepository repository)
    {
        this._repository = repository;
    }

    public void OnGet()
    {
        NomenclatorDto = new() { Name = "", Type = TypeOfNomenclator.ReasonOfDownload };
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            NomenclatorDto.Type = TypeOfNomenclator.ReasonOfDownload;
            await _repository.CreateNomenclatorAsync(NomenclatorDto);
            TempData["Created Reason"] = true;
            Response.Redirect("/Reason/Index");

            //clear the Form
            NomenclatorDto.Name = "";
            NomenclatorDto.Type = TypeOfNomenclator.ReasonOfDownload;
            ModelState.Clear();
        }
    }
    
}