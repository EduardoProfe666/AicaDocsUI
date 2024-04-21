using System.ComponentModel.DataAnnotations;
using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using Microsoft.Extensions.Options;

namespace AicaDocsUI.Pages.PagesModelsData.Models.Downloads;

public class DownloadCreatedModel
{
    [Required(ErrorMessage = "El formato es requerido"), ValidateEnumeratedItems]
    public required Format Format { get; set; }
    [Required(ErrorMessage = "El usuario es requerido"), MaxLength(64, ErrorMessage = "El máximo de caracteres es 64")]
    public required string Username { get; set; }
    [Required(ErrorMessage = "El documento es requerido")]
    public required int DocumentId { get; set; }
    [Required(ErrorMessage = "La razón de descarga es requerida")]
    public required int ReasonId { get; set; }
    
}