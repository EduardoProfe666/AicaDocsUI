using AicaDocsUI.Pages.PagesModelsData.Models.Documents;
using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Documents;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Repositories.Documents;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Documents;

public class Create : PageModel
{
    private readonly INomenclatorRepository _nomenclatorRepository;
    private readonly IDocumentRepository _documentRepository;
    private readonly IAuthRepository _authRepository;

    public IEnumerable<SelectListItem> ListTypeId { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> ListProcessId { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> ListScopeId { get; set; } = new List<SelectListItem>();
    

    [BindProperty] public DocumentCreatedModel DocumentCreatedModel { get; set; }

    public Create(INomenclatorRepository repository, IDocumentRepository downloadRepository, IAuthRepository authRepository)
    {
        _nomenclatorRepository = repository;
        _documentRepository = downloadRepository;
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
        
        if (c != UserRole.Worker)
        {
            return RedirectToPage("/Error", new { code = 403 });
        }
        
        DocumentCreatedModel = new DocumentCreatedModel
        {
            Code = "", Edition = 1, Pdf = null, Word = null, Title = "", ProcessId = 2, ScopeId = 2, TypeId = 2,
            DateOfValidity = DateTimeOffset.Now
        };

        var dataType = await _nomenclatorRepository.GetNomenclatorsByTypeAsync((int)TypeOfNomenclator.TypeOfDocument);
        var dataProcess =
            await _nomenclatorRepository.GetNomenclatorsByTypeAsync((int)TypeOfNomenclator.ProcessOfDocument);
        var dataScope = await _nomenclatorRepository.GetNomenclatorsByTypeAsync((int)TypeOfNomenclator.ScopeOfDocument);

        ListTypeId = dataType!
            .Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });

        ListProcessId = dataProcess!
            .Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });

        ListScopeId = dataScope!
            .Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });
        return Page();
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var download = await _documentRepository.CreateDocumentAsync(new DocumentCreatedDto()
            {
                Code = DocumentCreatedModel.Code, Edition = DocumentCreatedModel.Edition, Pdf = DocumentCreatedModel.Pdf,
                Word = DocumentCreatedModel.Word, Title = DocumentCreatedModel.Title, ProcessId = DocumentCreatedModel.ProcessId,
                ScopeId = DocumentCreatedModel.ScopeId, TypeId = DocumentCreatedModel.TypeId, DateOfValidity = DocumentCreatedModel.DateOfValidity
            });
            TempData["Created Document"] = download;
            Response.Redirect("/Documents/Index");

            //clear the Form
            ModelState.Clear();
        }
        else
        {
            var dataType = await _nomenclatorRepository.GetNomenclatorsByTypeAsync((int)TypeOfNomenclator.TypeOfDocument);
            var dataProcess =
                await _nomenclatorRepository.GetNomenclatorsByTypeAsync((int)TypeOfNomenclator.ProcessOfDocument);
            var dataScope = await _nomenclatorRepository.GetNomenclatorsByTypeAsync((int)TypeOfNomenclator.ScopeOfDocument);

            ListTypeId = dataType!
                .Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                });

            ListProcessId = dataProcess!
                .Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                });

            ListScopeId = dataScope!
                .Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                });
        }
    }
}