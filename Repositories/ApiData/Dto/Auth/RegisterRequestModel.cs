using System.ComponentModel.DataAnnotations;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using Microsoft.Extensions.Options;

namespace AicaDocsUI.Repositories.ApiData.Dto.Auth;

public class RegisterRequestModel
{
    [Required(ErrorMessage = "El correo es requerido"),
     EmailAddress(ErrorMessage = "El correo debe ser válido")]
    public required string Email { get; set; }
    
    [Required(ErrorMessage = "El nombre debe ser requerido"),
     MaxLength(64, ErrorMessage = "La cantidad máxima de caracteres es 64")]
    public required string FullName { get; set; }
    
    [Required(ErrorMessage = "El rol de usuario es requerido")]
    public required UserRole Role { get; set; }
}