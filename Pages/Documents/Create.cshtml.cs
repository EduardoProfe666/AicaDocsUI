using AicaDocsUI.Dto.Documents;
using AicaDocsUI.Models;
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
    
    public IEnumerable<Nomenclator> TypesDoc { get; set; }
    public IEnumerable<Nomenclator> ProcessDoc { get; set; }
    public IEnumerable<Nomenclator> ScopeDoc { get; set; }

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
        TypesDoc = dataType!;

        ListProcessId = dataProcess!
            .Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });
        ProcessDoc = dataProcess!;

        ListScopeId = dataScope!
            .Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });
        ScopeDoc = dataScope!;
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
            ListTypeId = TypesDoc!
                .Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                });

            var data = await _nomenclatorRepository.GetNomenclatorsByTypeAsync((int)TypeOfNomenclator.ReasonOfDownload);

            ListProcessId = ProcessDoc
                .Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                });
            
            ListScopeId = ScopeDoc
                .Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                });
        }
    }
}