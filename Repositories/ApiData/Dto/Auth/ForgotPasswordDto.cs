using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Models.Auth;

public class ForgotPasswordDto
{
    [Required(ErrorMessage = "El correo es requerido"),
     EmailAddress(ErrorMessage = "El correo debe ser válido")]
    public required string Email { get; set; }
    public required string UrlView { get; init; }
}