using System.ComponentModel.DataAnnotations;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using Microsoft.Extensions.Options;

namespace AicaDocsUI.Repositories.ApiData.Dto.Auth;

public class RegisterRequestDto
{
    [Required(ErrorMessage = "El correo es requerido"),
     EmailAddress(ErrorMessage = "El correo debe ser válido")]
    public required string Email { get; init; }
    
    [Required(ErrorMessage = "El nombre debe ser requerido"),
     MaxLength(64, ErrorMessage = "La cantidad máxima de caracteres es 64")]
    public required string FullName { get; init; }
    
    [Required(ErrorMessage = "El rol de usuario es requerido"),
     ValidateEnumeratedItems]
    public required UserRole Role { get; init; }
    
    public required string UrlView { get; init; }
}