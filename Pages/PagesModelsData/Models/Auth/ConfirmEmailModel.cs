﻿using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Pages.PagesModelsData.Models.Auth;

public class ConfirmEmailModel
{
    [Required(ErrorMessage = "La contraseña nueva es requerida"),
     MinLength(6, ErrorMessage = "La mínima extensión de caracteres es de 6"),
     MaxLength(32, ErrorMessage = "La máxima extensión de caracteres es de 32"),
     RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula y un número")]
    public required string Password { get; set; }
}