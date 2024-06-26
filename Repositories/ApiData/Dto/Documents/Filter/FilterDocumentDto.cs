using AicaDocsUI.Repositories.ApiData.Dto.FilterCommons;

namespace AicaDocsUI.Repositories.ApiData.Dto.Documents.Filter;

public class FilterDocumentDto
{
    public required string? Code { get; set; }
    public required string? Title { get; set; }
    public required int? Edition { get; set; }
    public required int? Pages { get; set; }
    public required DateTimeOffset? DateOfValidity { get; set; }
    public required int? TypeId { get; set; }
    public required int? ProcessId { get; set; }
    public required int? ScopeId { get; set; }
    
    public required string? UserEmail { get; set; }

    public PaginationParams PaginationParams { get; set; } = new PaginationParams();
    public SortByDocument SortBy { get; set; } = SortByDocument.Id;
    public SortOrder SortOrder { get; set; } = SortOrder.Asc;
    public DateComparator DateComparator { get; set; } = DateComparator.Equal;
}