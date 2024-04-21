using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Repositories.ApiData.Dto.Auth;

public class LoginRequestDto
{
    [Required(ErrorMessage = "El correo es requerido"),
     EmailAddress(ErrorMessage = "El correo debe ser válido")]
    public required string Email { get; set; }
    
    [Required(ErrorMessage = "La contraseña es requerida"),
     MinLength(6, ErrorMessage = "La mínima extensión de caracteres es de 6"),
     MaxLength(32, ErrorMessage = "La máxima extensión de caracteres es de 32"),
     RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula y un número")]
    public required string Password { get; set; }
}