using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Repositories.ApiData.Dto.Auth;

public class ForgotPasswordDto
{
    [Required(ErrorMessage = "El correo es requerido"),
     EmailAddress(ErrorMessage = "El correo debe ser válido")]
    public required string Email { get; set; }
    public required string UrlView { get; init; }
}