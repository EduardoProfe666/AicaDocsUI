using AicaDocsApi.Dto.Downloads.Filter;
using AicaDocsApi.Dto.FilterCommons;
using AicaDocsApi.Dto.PaginationCommons;
using AicaDocsUI.Dto.Downloads.Filter;
using AicaDocsUI.Extensions;
using AicaDocsUI.Models;
using AicaDocsUI.Repositories.Downloads;
using AicaDocsUI.Repositories.Nomenclators;
using AicaDocsUI.Repositories.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Downloads;

public class Index : PageModel
{
    private readonly IPaginationRepository _paginationRepository;
    private readonly IDownloadRepository _downloadRepository;
    private readonly INomenclatorRepository _nomenclatorRepository;

    public int PageTotal { get; set; }

    public FilterDownloadDto Filter { get; set; }
    
    [BindProperty]
    public Format? Format { get; set; }
    [BindProperty]
    public DateTimeOffset? DateDownload { get; set; }
    [BindProperty]
    public string? Username { get; set; }
    [BindProperty]
    public int? DocumentId { get; set; }
    [BindProperty]
    public int? ReasonId { get; set; }
    [BindProperty]
    public SortByDownload SortBy { get; set; }
    [BindProperty]
    public SortOrder SortOrder { get; set; }
    [BindProperty]
    public DateComparator DateComparator { get; set; }
    [BindProperty]
    public int PageNumber { get; set; }

    public IEnumerable<SelectListItem> ListFormat { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> ListDateComparator { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> ListReasonId { get; set; } = new List<SelectListItem>();

    public IEnumerable<Nomenclator> Reasons { get; set; }

    public Index(IPaginationRepository repository, IDownloadRepository downloadRepository,
        INomenclatorRepository nomenclatorRepository)
    {
        _paginationRepository = repository;
        _downloadRepository = downloadRepository;
        _nomenclatorRepository = nomenclatorRepository;
    }

    public IEnumerable<Download> Downloads { get; set; } = new List<Download>();

    public async Task OnGetAsync(Format? format, DateTimeOffset? dateDownload, string? username, int? documentId,
        int? reasonId, SortByDownload? sortBy, SortOrder? sortOrder, DateComparator? dateComparator, int? pageNumber)

    {
        Filter = new()
        {
            Format = format, Username = username, DateDownload = dateDownload, DocumentId = documentId,
            ReasonId = reasonId, DateComparator = DateComparator.Equal, SortOrder = SortOrder.Asc,
            SortBy = SortByDownload.DateDownload, PaginationParams = new PaginationParams() { PageSize = 6, PageNumber = 1 }
        };

        if (sortBy != null)
            Filter.SortBy = sortBy.Value;
        if (sortOrder != null)
            Filter.SortOrder = sortOrder.Value;
        if (dateComparator != null)
            Filter.DateComparator = dateComparator.Value;
        if (pageNumber != null)
            Filter.PaginationParams.PageNumber = pageNumber.Value;
        if (Filter.DateDownload != null)
            Filter.DateDownload = Filter.DateDownload.Value.UtcDateTime;

        PageTotal = (await _paginationRepository.GetCountPages((int)TypeOfDataSet.Downloads,
            Filter.PaginationParams.PageSize))!.Value;

        ListFormat = Enum.GetValues(typeof(Format))
            .Cast<Format>()
            .Select(v => new SelectListItem
            {
                Text = v.GetDescription(),
                Value = v.ToString()
            });

        ListDateComparator = Enum.GetValues(typeof(DateComparator))
            .Cast<DateComparator>()
            .Select(v => new SelectListItem
            {
                Text = v.GetDescription(),
                Value = v.ToString()
            });

        var data = await _nomenclatorRepository.GetNomenclatorsByTypeAsync((int)TypeOfNomenclator.ReasonOfDownload);

        ListReasonId = data!
            .Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });
        Reasons = data!;
        Downloads = (await _downloadRepository.GetDownloadsFilter(Filter))!;

        Format = Filter.Format;
        DateDownload = Filter.DateDownload;
        Username = Filter.Username;
        DocumentId = Filter.DocumentId;
        ReasonId = Filter.ReasonId;
        SortBy = Filter.SortBy;
        SortOrder = Filter.SortOrder;
        DateComparator = Filter.DateComparator;
        PageNumber = Filter.PaginationParams.PageNumber;
    }
}