using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Dto.Documents;

public class DocumentCreatedDto
{ 
    [Required(ErrorMessage = "El título es requerido"), MaxLength(64, ErrorMessage = "La máxima extensión de caracteres es de 64")]
    public required string Title { get; set; }
    [Required(ErrorMessage = "El código es requerido"), MaxLength(64, ErrorMessage = "La máxima extensión de caracteres es de 64")]
    public required string Code { get; set; }
    [Required(ErrorMessage = "La edición es requerida")]
    public required int Edition { get; set; }
    [Required(ErrorMessage = "La fecha es requerida")]
    public required DateTimeOffset DateOfValidity { get; set; }
    
    [Required(ErrorMessage = "El tipo es requerido")]
    public required int TypeId { get; set; }
    [Required(ErrorMessage = "El proceso es requerido")]
    public required int ProcessId { get; set; }
    [Required(ErrorMessage = "El alcance es requerido")]
    public required int ScopeId { get; set; }

    [Required(ErrorMessage = "El pdf es requerido")]
    public required IFormFile Pdf { get; set; }

    [Required(ErrorMessage = "El word es requerido")]
    public required IFormFile Word { get; set; }
}