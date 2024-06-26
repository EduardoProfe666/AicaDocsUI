using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Repositories.ApiData.Dto.Documents;

public class DocumentCreatedDto
{
    public required string Title { get; set; }
    
    public required string Code { get; set; }
    
    public required int Edition { get; set; }
    
    public required DateTimeOffset DateOfValidity { get; set; }
    
    public required int TypeId { get; set; }
    
    public required int ProcessId { get; set; }
    
    public required int ScopeId { get; set; }

    public required IFormFile Pdf { get; set; }
    
    public required IFormFile Word { get; set; }
}