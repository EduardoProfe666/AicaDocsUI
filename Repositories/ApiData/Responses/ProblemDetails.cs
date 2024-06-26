namespace AicaDocsUI.Repositories.ApiData.Responses;

public class ProblemDetails
{
    public required int? Status { get; set; }
    public required IDictionary<string, string[]> Errors { get; set; }
}