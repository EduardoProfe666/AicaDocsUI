using AicaDocsUI.Pages.PagesModelsData.Models.Nomenclators;
using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Reports.Reason;

public class Edit : PageModel
{
    private readonly INomenclatorRepository _repository;
    private readonly IAuthRepository _authRepository;
    [BindProperty] public IntegerWrapperValue Id11 { get; set; }
    public string Name { get; set; }
    [BindProperty] public NomenclatorPutModel NomenclatorModel { get; set; }

    public Edit(INomenclatorRepository repository, IAuthRepository authRepository)
    {
        this._repository = repository;
        _authRepository = authRepository;
    }

    public async Task<IActionResult> OnGetAsync(int id)
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
        
        Id11 = new IntegerWrapperValue{Id = id};
        var data = await _repository.GetNomenclatorAsync((int)TypeOfNomenclator.ReasonOfDownload, id);
        Name = data.Name;
        NomenclatorModel = new NomenclatorPutModel() { Name = Name };
        return Page();
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var b = await _repository.PutNomenclatorAsync(Id11.Id, new NomenclatorPutDto(){Name = NomenclatorModel.Name});
            
            TempData["Edited Reason"] = b;
            TempData["Error Unique"] = !b;
            Response.Redirect("/Reports/Reason/Index");
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