using AicaDocsUI.Extensions;
using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Downloads;
using AicaDocsUI.Repositories.ApiData.Dto.Downloads.Filter;
using AicaDocsUI.Repositories.ApiData.Dto.FilterCommons;
using AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;
using AicaDocsUI.Repositories.Documents;
using AicaDocsUI.Repositories.Downloads;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Reports.Downloads;

public class Index : PageModel
{
    private readonly IDownloadRepository _downloadRepository;
    private readonly INomenclatorRepository _nomenclatorRepository;
    private readonly IDocumentRepository _documentRepository;

    public int PageTotal { get; set; }

    public FilterDownloadDto Filter { get; set; }

    [BindProperty] public Format? Format { get; set; }
    [BindProperty] public DateTimeOffset? DateDownload { get; set; }
    [BindProperty] public string? Username { get; set; }
    [BindProperty] public int? DocumentId { get; set; }
    [BindProperty] public int? ReasonId { get; set; }
    [BindProperty] public SortByDownload SortBy { get; set; }
    [BindProperty] public SortOrder SortOrder { get; set; }
    [BindProperty] public DateComparator DateComparator { get; set; }
    [BindProperty] public int PageNumber { get; set; }

    public IEnumerable<SelectListItem> ListFormat { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> ListDateComparator { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> ListReasonId { get; set; } = new List<SelectListItem>();

    public IEnumerable<NomenclatorDto> Reasons { get; set; }

    public Index(IDownloadRepository downloadRepository,
        INomenclatorRepository nomenclatorRepository, IDocumentRepository documentRepository)
    {
        _downloadRepository = downloadRepository;
        _nomenclatorRepository = nomenclatorRepository;
        _documentRepository = documentRepository;
    }

    public List<DownloadDtoDocument> Downloads { get; set; } = new();

    public async Task OnGetAsync(Format? format, DateTimeOffset? dateDownload, string? username, int? documentId,
        int? reasonId, SortByDownload? sortBy, SortOrder? sortOrder, DateComparator? dateComparator, int? pageNumber)

    {
        Filter = new()
        {
            Format = format, UserEmail = username, DateDownload = dateDownload, DocumentId = documentId,
            ReasonId = reasonId, DateComparator = DateComparator.Equal, SortOrder = SortOrder.Asc,
            SortBy = SortByDownload.DateDownload,
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
        if (Filter.DateDownload != null)
            Filter.DateDownload = Filter.DateDownload.Value.UtcDateTime;

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

        var data1 = (await _downloadRepository.GetDownloadsFilterAsync(Filter))!;
        var downloads = data1!.Response;
        PageTotal = data1.TotalPages;

        Format = Filter.Format;
        DateDownload = Filter.DateDownload;
        Username = Filter.UserEmail;
        DocumentId = Filter.DocumentId;
        ReasonId = Filter.ReasonId;
        SortBy = Filter.SortBy;
        SortOrder = Filter.SortOrder;
        DateComparator = Filter.DateComparator;
        PageNumber = Filter.PaginationParams.PageNumber;
        foreach (var download in downloads)
        {
            var doc = (await _documentRepository.GetDocumentByIdAsync(download.DocumentId))!;
            Downloads.Add(new DownloadDtoDocument
            {
                Id= download.Id,
                CodeEdition = $"{doc.Code}-{doc.Edition}",
                Format = download.Format,
                UserEmail = download.UserEmail,
                DocumentId = download.DocumentId,
                ReasonId = download.ReasonId,
                DateOfDownload = download.DateOfDownload
            });
        }
    }

    public class DownloadDtoDocument: DownloadDto
    {
        public required string CodeEdition { get; set; }
    }
    
}
