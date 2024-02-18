using AicaDocsApi.Dto.Nomenclators;
using AicaDocsUI.Dto.Nomenclators;
using AicaDocsUI.Extensions;
using AicaDocsUI.Models;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Nomenclators;

public class Edit : PageModel
{
    private readonly INomenclatorRepository _repository;
    private int Id { get; set; }
    private string Name { get; set; }
    [BindProperty] public NomenclatorPatchDto NomenclatorDto { get; set; }

    public Edit(INomenclatorRepository repository)
    {
        this._repository = repository;
    }

    public async Task OnGetAsync(TypeOfNomenclator typeOfNomenclator, int id)
    {
        Id = id;
        var data = await _repository.GetNomenclatorAsync((int)typeOfNomenclator, id);
        Name = data.Name;
        NomenclatorDto = new NomenclatorPatchDto() { Name = Name };
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await _repository.PatchNomenclatorAsync(Id, NomenclatorDto);
            TempData["Update Nomenclator"] = true;
            Response.Redirect("/Nomenclators/Index");

            //clear the Form
            NomenclatorDto.Name = "";
            ModelState.Clear();
        }
        
    }
}