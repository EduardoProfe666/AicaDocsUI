using AicaDocsApi.Dto.Nomenclators;
using AicaDocsUI.Dto.Nomenclators;
using AicaDocsUI.Extensions;
using AicaDocsUI.Models;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Reason;

public class Edit : PageModel
{
    private readonly INomenclatorRepository _repository;
    [BindProperty] public IntegerWrapperValue Id11 { get; set; }
    public string Name { get; set; }
    [BindProperty] public NomenclatorPatchDto NomenclatorDto { get; set; }

    public Edit(INomenclatorRepository repository)
    {
        this._repository = repository;
    }

    public async Task OnGetAsync(int id)
    {
        Id11 = new IntegerWrapperValue{Id = id};
        var data = await _repository.GetNomenclatorAsync((int)TypeOfNomenclator.ReasonOfDownload, id);
        Name = data.Name;
        NomenclatorDto = new NomenclatorPatchDto() { Name = Name };
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await _repository.PatchNomenclatorAsync(Id11.Id, NomenclatorDto);
            TempData["Edited Reason"] = true;
            Response.Redirect("/Reason/Index");
            //clear the Form
            NomenclatorDto.Name = "";
            ModelState.Clear();
        }
        
    }
    public class IntegerWrapperValue
    {
        public int Id { get; set; }
    }
}