using AicaDocsApi.Dto.Documents.Filter;
using AicaDocsApi.Dto.FilterCommons;
using AicaDocsUI.Extensions;
using AicaDocsUI.Models;
using AicaDocsUI.Repositories.Documents;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Documents;

public class Index : PageModel
{
    private readonly INomenclatorRepository _nomenclatorRepository;
    private readonly IDocumentRepository _documentRepository;
    
    public bool ShowCreated { get; set; }
    public bool ShowDownload { get; set; }
    
    public int PageTotal { get; set; }

    public FilterDocumentDto Filter { get; set; }

    [BindProperty] public string? Code { get; set; }
    [BindProperty] public DateTimeOffset? DateOfValidity { get; set; }
    [BindProperty] public string? Title { get; set; }
    [BindProperty] public int? Edition { get; set; }
    [BindProperty] public int? Pages { get; set; }
    [BindProperty] public int? TypeId { get; set; }
    [BindProperty] public int? ProcessId { get; set; }
    [BindProperty] public int? ScopeId { get; set; }
    [BindProperty] public SortByDocument SortBy { get; set; }
    [BindProperty] public SortOrder SortOrder { get; set; }
    [BindProperty] public DateComparator DateComparator { get; set; }
    [BindProperty] public int PageNumber { get; set; }

    public IEnumerable<SelectListItem> ListDateComparator { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> ListTypeId { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> ListProcessId { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> ListScopeId { get; set; } = new List<SelectListItem>();

    public IEnumerable<NomenclatorDto> TypesDoc { get; set; }
    public IEnumerable<NomenclatorDto> ProcessDoc { get; set; }
    public IEnumerable<NomenclatorDto> ScopeDoc { get; set; }

    public Index(INomenclatorRepository nomenclatorRepository,
        IDocumentRepository documentRepository)
    {
        _nomenclatorRepository = nomenclatorRepository;
        _documentRepository = documentRepository;
    }

    public IEnumerable<DocumentDto> Documents { get; set; } = new List<DocumentDto>();

    public async Task OnGetAsync(string? code, DateTimeOffset? dateOfValidity, string? title, int? edition, int? pages,
        int? typeId,
        int? processId, int? scopeId, SortByDocument? sortBy, SortOrder? sortOrder, DateComparator? dateComparator,
        int? pageNumber)

    {
        Filter = new()
        {
            Code = code, Edition = edition, DateOfValidity = dateOfValidity,
            Pages = pages, Title = title, ProcessId = processId, ScopeId = scopeId, TypeId = typeId,
            DateComparator = DateComparator.Equal, SortOrder = SortOrder.Asc,
            SortBy = SortByDocument.Title,
            PaginationParams = new PaginationParams() { PageSize = 5, PageNumber = 1 }
        };

        if (sortBy != null)
            Filter.SortBy = sortBy.Value;
        if (sortOrder != null)
            Filter.SortOrder = sortOrder.Value;
        if (dateComparator != null)
            Filter.DateComparator = dateComparator.Value;
        if (pageNumber != null)
            Filter.PaginationParams.PageNumber = pageNumber.Value;

        ListDateComparator = Enum.GetValues(typeof(DateComparator))
            .Cast<DateComparator>()
            .Select(v => new SelectListItem
            {
                Text = v.GetDescription(),
                Value = v.ToString()
            });

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

        Filter.DateOfValidity = Filter.DateOfValidity?.UtcDateTime;

        var data1 = (await _documentRepository.FilterDocuments(Filter))!;
        Documents = data1!.Data;
        PageTotal = data1.TotalPages;

        Code = Filter.Code;
        Edition = Filter.Edition;
        DateOfValidity = Filter.DateOfValidity;
        Title = Filter.Title;
        Pages = Filter.Pages;
        TypeId = Filter.TypeId;
        ProcessId = Filter.ProcessId;
        ScopeId = Filter.ScopeId;
        SortBy = Filter.SortBy;
        SortOrder = Filter.SortOrder;
        DateComparator = Filter.DateComparator;
        PageNumber = Filter.PaginationParams.PageNumber;
        
        ShowCreated = TempData["Created Document"] as bool? ?? false;
        
        TempData["Created Document"] = false;
    }
}