using AicaDocsUI.Dto.Nomenclators;
using AicaDocsUI.Extensions;
using AicaDocsUI.Models;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Type;

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
        NomenclatorDto = new() { Name = "", Type = TypeOfNomenclator.TypeOfDocument };
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            NomenclatorDto.Type = TypeOfNomenclator.TypeOfDocument;
            await _repository.CreateNomenclatorAsync(NomenclatorDto);
            TempData["Created Type"] = true;
            Response.Redirect("/Type/Index");

            //clear the Form
            NomenclatorDto.Name = "";
            NomenclatorDto.Type = TypeOfNomenclator.TypeOfDocument;
            ModelState.Clear();
        }
    }
    
}