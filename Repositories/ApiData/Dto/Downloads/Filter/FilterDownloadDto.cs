using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.FilterCommons;

namespace AicaDocsUI.Repositories.ApiData.Dto.Downloads.Filter;

public class FilterDownloadDto
{
    public required Format? Format { get; set; }
    public required DateTimeOffset? DateDownload { get; set; }
    public required string? UserEmail { get; set; }
    public required int? DocumentId { get; set; }
    public required int? ReasonId { get; set; }

    public PaginationParams PaginationParams { get; set; } = new PaginationParams();
    public SortByDownload SortBy { get; set; } = SortByDownload.Id;
    public SortOrder SortOrder { get; set; } = SortOrder.Asc;
    public DateComparator DateComparator { get; set; } = DateComparator.Equal;
}
