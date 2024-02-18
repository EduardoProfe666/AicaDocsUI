using System.ComponentModel.DataAnnotations;
using AicaDocsUI.Models;
using Microsoft.Extensions.Options;

namespace AicaDocsUI.Dto.Nomenclators;

public class NomenclatorCreatedDto
{
    [Required(ErrorMessage = "El nombre es requerido"), MaxLength(64, ErrorMessage = "El nombre debe tener una longitud menor o igual que 64 caracteres")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "El tipo de nomenclador es requerido"), ValidateEnumeratedItems] public required TypeOfNomenclator Type { get; set; }
}