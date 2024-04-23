using AicaDocsUI.Pages.PagesModelsData.Models.Nomenclators;
using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Reports.Type;

public class Create : PageModel
{
    private readonly INomenclatorRepository _repository;
    private readonly IAuthRepository _authRepository;

    [BindProperty] public NomenclatorCreatedModel NomenclatorModel { get; set; }

    public Create(INomenclatorRepository repository, IAuthRepository authRepository)
    {
        this._repository = repository;
        _authRepository = authRepository;
    }

    public async Task<IActionResult> OnGet()
    {
        bool b = await _authRepository.IsLoginAdvanceAsync();

        if (!b)
        {
            TempData["Unauthorized"] = true;
            return RedirectToPage("/Account/Login");
        }

        var c = await _authRepository.GetUserRoleAsync();
        if (c == null)
            return RedirectToPage();
        
        if (c != UserRole.Admin)
        {
            return RedirectToPage("/Error", new { code = 403 });
        }
        
        NomenclatorModel = new() { Name = "", Type = TypeOfNomenclator.TypeOfDocument };

        return Page();
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            NomenclatorModel.Type = TypeOfNomenclator.TypeOfDocument;
            var b = await _repository.CreateNomenclatorAsync(new NomenclatorCreatedDto(){Name = NomenclatorModel.Name,Type = NomenclatorModel.Type});
            
            TempData["Created Type"] = b;
            TempData["Error Unique"] = !b;
            Response.Redirect("/Reports/Type/Index");

            //clear the Form
            NomenclatorModel.Name = "";
            NomenclatorModel.Type = TypeOfNomenclator.TypeOfDocument;
            ModelState.Clear();
        }
    }
    
}