using System.ComponentModel.DataAnnotations;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;

namespace AicaDocsUI.Pages.PagesModelsData.Models.Auth;

public class RegisterRequestModel
{
    [Required(ErrorMessage = "El correo es requerido"),
     RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
         ErrorMessage = "El correo debe ser válido")]
    public required string Email { get; set; }
    
    [Required(ErrorMessage = "El nombre debe ser requerido"),
     MaxLength(64, ErrorMessage = "La cantidad máxima de caracteres es 64")]
    public required string FullName { get; set; }
    
    [Required(ErrorMessage = "El rol de usuario es requerido")]
    public required UserRole Role { get; set; }
}