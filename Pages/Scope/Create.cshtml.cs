using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Scope;

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
        NomenclatorDto = new() { Name = "", Type = TypeOfNomenclator.ScopeOfDocument };
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            NomenclatorDto.Type = TypeOfNomenclator.ScopeOfDocument;
            await _repository.CreateNomenclatorAsync(NomenclatorDto);
            TempData["Created Scope"] = true;
            Response.Redirect("/Scope/Index");

            //clear the Form
            NomenclatorDto.Name = "";
            NomenclatorDto.Type = TypeOfNomenclator.ScopeOfDocument;
            ModelState.Clear();
        }
    }
    
}