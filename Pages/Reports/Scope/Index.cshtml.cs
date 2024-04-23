using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Reports.Scope;

public class Index : PageModel
{
    private readonly INomenclatorRepository _repository;
    private readonly IAuthRepository _authRepository;
    public bool ShowCreated { get; set; }
    public bool ShowEdited { get; set; }
    public bool ShowDeleted { get; set; }
    
    public bool ShowErrorUnique { get; set; }
    
    public bool ShowErrorDelete { get; set; }


    public Index(INomenclatorRepository repository, IAuthRepository authRepository)
    {
        this._repository = repository;
        _authRepository = authRepository;
    }

    public IEnumerable<NomenclatorDto> Nomenclators { get; set; } = new List<NomenclatorDto>();

    public async Task<IActionResult> OnGetAsync()
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
        
        var data = await _repository.GetNomenclatorsByTypeAsync((int)TypeOfNomenclator.ScopeOfDocument);
        if (data != null)
            Nomenclators = data;
        
        ShowCreated = TempData["Created Scope"] as bool? ?? false;
        ShowEdited = TempData["Edited Scope"] as bool? ?? false;
        ShowDeleted = TempData["Deleted Process"] as bool? ?? false;
        ShowErrorDelete = TempData["Error to delete"] as bool? ?? false;
        ShowErrorUnique = TempData["Error Unique"] as bool? ?? false;
        
        TempData["Created Scope"] = false;
        TempData["Edited Scope"] = false;
        TempData["Deleted Process"] = false;
        TempData["Error to delete"] = false;
        TempData["Error Unique"] = false;

        return Page();
    }
    
    public async Task<IActionResult> OnGetDeleteAsync(int id)
    {
        var result = await _repository.DeleteNomenclatorAsync((int)TypeOfNomenclator.ScopeOfDocument, id);
        if (result)
        {
            TempData["Deleted Process"] = true;
        }
        else
        {
            TempData["Error to delete"] = true; 
        }

        return RedirectToPage();
    }
    
}