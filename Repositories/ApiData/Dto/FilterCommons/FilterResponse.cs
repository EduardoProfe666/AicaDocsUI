namespace AicaDocsUI.Repositories.ApiData.Dto.FilterCommons;

public class FilterResponse<T>
{
    public required IEnumerable<T> Response { get; set; }
    public required int TotalPages { get; set; }
}