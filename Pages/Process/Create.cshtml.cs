using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Process;

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
        NomenclatorDto = new() { Name = "", Type = TypeOfNomenclator.ProcessOfDocument };
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            NomenclatorDto.Type = TypeOfNomenclator.ProcessOfDocument;
            await _repository.CreateNomenclatorAsync(NomenclatorDto);
            TempData["Created Process"] = true;
            Response.Redirect("/Process/Index");

            //clear the Form
            NomenclatorDto.Name = "";
            NomenclatorDto.Type = TypeOfNomenclator.ProcessOfDocument;
            ModelState.Clear();
        }
    }
    
}