using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Type;

public class Edit : PageModel
{
    private readonly INomenclatorRepository _repository;
    [BindProperty] public IntegerWrapperValue Id11 { get; set; }
    public string Name { get; set; }
    [BindProperty] public NomenclatorPutDto NomenclatorDto { get; set; }

    public Edit(INomenclatorRepository repository)
    {
        this._repository = repository;
    }

    public async Task OnGetAsync(int id)
    {
        Id11 = new IntegerWrapperValue{Id = id};
        var data = await _repository.GetNomenclatorAsync((int)TypeOfNomenclator.TypeOfDocument, id);
        Name = data.Name;
        NomenclatorDto = new NomenclatorPutDto() { Name = Name };
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await _repository.PutNomenclatorAsync(Id11.Id, NomenclatorDto);
            TempData["Edited Type"] = true;
            Response.Redirect("/Type/Index");
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