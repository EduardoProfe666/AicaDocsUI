using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Documents;
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

    public IEnumerable<SelectListItem> ListTypeId { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> ListProcessId { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> ListScopeId { get; set; } = new List<SelectListItem>();
    

    [BindProperty] public DocumentCreatedDto DocumentCreatedDto { get; set; }

    public Create(INomenclatorRepository repository, IDocumentRepository downloadRepository)
    {
        _nomenclatorRepository = repository;
        _documentRepository = downloadRepository;
    }

    public async Task OnGetAsync(int id)
    {
        DocumentCreatedDto = new DocumentCreatedDto
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
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var download = await _documentRepository.CreateDocument(DocumentCreatedDto);
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