using System.ComponentModel.DataAnnotations;

namespace AicaDocsApi.Dto.Nomenclators;

public class NomenclatorPatchDto
{
    [Required(ErrorMessage = "El nombre es requerido"),
     MaxLength(64, ErrorMessage = "El nombre debe tener una longitud menor o igual que 64 caracteres")]
    public required string Name { get; set; }
}