using System.ComponentModel.DataAnnotations;
using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using Microsoft.Extensions.Options;

namespace AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;

public class NomenclatorCreatedDto
{
    [Required(ErrorMessage = "El nombre es requerido"), MaxLength(64, ErrorMessage = "El nombre debe tener una longitud menor o igual que 64 caracteres")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "El tipo de nomenclador es requerido"), ValidateEnumeratedItems] public required TypeOfNomenclator Type { get; set; }
}