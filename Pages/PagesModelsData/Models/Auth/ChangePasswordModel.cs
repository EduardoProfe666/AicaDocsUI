using System.ComponentModel.DataAnnotations;
using AicaDocsUI.Pages.PagesModelsData.Validations;

namespace AicaDocsUI.Pages.PagesModelsData.Models.Auth;

public class ChangePasswordModel
{
    [Required(ErrorMessage = "La contraseña antigua es requerida")]
    public required string OldPassword { get; set; }

    [Required(ErrorMessage = "La contraseña nueva es requerida"),
     MinLength(6, ErrorMessage = "La mínima extensión de caracteres es de 6"),
     MaxLength(32, ErrorMessage = "La máxima extensión de caracteres es de 32"),
     RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula y un número"),
     NotEqualTo("OldPassword", ErrorMessage = "La nueva contraseña no puede ser igual a la antigua.")]
    public required string NewPassword { get; set; }
}