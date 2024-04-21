using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Pages.PagesModelsData.Models.Auth;

public class ForgotPasswordModel
{
    [Required(ErrorMessage = "El correo es requerido"),
    RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "El correo debe ser válido")]
    public required string Email { get; set; }
}