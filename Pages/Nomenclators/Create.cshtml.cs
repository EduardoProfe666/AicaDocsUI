using AicaDocsUI.Dto.Nomenclators;
using AicaDocsUI.Extensions;
using AicaDocsUI.Models;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Nomenclators;

public class Create : PageModel
{
    private readonly INomenclatorRepository _repository;
    public IEnumerable<SelectListItem> ListItems { get; set; }

    [BindProperty] public NomenclatorCreatedDto NomenclatorDto { get; set; }

    public Create(INomenclatorRepository repository)
    {
        this._repository = repository;
    }

    public void OnGet()
    {
        ListItems = Enum.GetValues(typeof(TypeOfNomenclator))
            .Cast<TypeOfNomenclator>()
            .Select(v => new SelectListItem
            {
                Text = v.GetDescription(),
                Value = v.ToString()
            });
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await _repository.CreateNomenclatorAsync(NomenclatorDto);
            TempData["Created Nomenclator"] = true;
            Response.Redirect("/Nomenclators/Index");

            //clear the Form
            NomenclatorDto.Name = "";
            NomenclatorDto.Type = TypeOfNomenclator.ProcessOfDocument;
            ModelState.Clear();
        }
        else
        {
            ListItems = Enum.GetValues(typeof(TypeOfNomenclator))
                .Cast<TypeOfNomenclator>()
                .Select(v => new SelectListItem
                {
                    Text = v.GetDescription(),
                    Value = v.ToString()
                });
        }
    }
    
}