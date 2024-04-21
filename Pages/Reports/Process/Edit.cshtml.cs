using AicaDocsUI.Pages.PagesModelsData.Models.Nomenclators;
using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Reports.Process;

public class Edit : PageModel
{
    private readonly INomenclatorRepository _repository;
    [BindProperty] public IntegerWrapperValue Id11 { get; set; }
    public string Name { get; set; }
    [BindProperty] public NomenclatorPutModel NomenclatorModel { get; set; }

    public Edit(INomenclatorRepository repository)
    {
        this._repository = repository;
    }

    public async Task OnGetAsync(int id)
    {
        Id11 = new IntegerWrapperValue{Id = id};
        var data = await _repository.GetNomenclatorAsync((int)TypeOfNomenclator.ProcessOfDocument, id);
        Name = data.Name;
        NomenclatorModel = new NomenclatorPutModel() { Name = Name };
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            Console.WriteLine(Id11.Id);
            Console.WriteLine(NomenclatorModel.Name);
            await _repository.PutNomenclatorAsync(Id11.Id, new NomenclatorPutDto(){Name = NomenclatorModel.Name});
            TempData["Edited Process"] = true;
            Response.Redirect("/Process/Index");
            //clear the Form
            NomenclatorModel.Name = "";
            ModelState.Clear();
        }
        
    }
    public class IntegerWrapperValue
    {
        public int Id { get; set; }
    }
}