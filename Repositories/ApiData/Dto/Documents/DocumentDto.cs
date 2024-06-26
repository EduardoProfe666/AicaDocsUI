namespace AicaDocsUI.Repositories.ApiData.Dto.Documents;

public class DocumentDto
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string Title { get; set; }
    public required short Edition { get; set; }
    public required short Pages { get; set; }
    public required DateTimeOffset DateOfValidity { get; set; }

    public required int TypeId { get; set; }
    public required int ProcessId { get; set; }
    public required int ScopeId { get; set; }
    
    public required string UserEmail { get; set; }
}