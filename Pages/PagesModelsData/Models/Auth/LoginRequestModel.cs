using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Pages.PagesModelsData.Models.Auth;

public class LoginRequestModel
{
    [Required(ErrorMessage = "El correo es requerido"),
     RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
         ErrorMessage = "El correo debe ser válido")]
    public required string Email { get; set; }
    
    [Required(ErrorMessage = "La contraseña es requerida"),
     MaxLength(32, ErrorMessage = "La máxima extensión de caracteres es de 32")]
    public required string Password { get; set; }
}