using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Pages.PagesModelsData.Models.Nomenclators;

public class NomenclatorPutModel
{
    [Required(ErrorMessage = "El nombre es requerido"),
     MaxLength(64, ErrorMessage = "El nombre debe tener una longitud menor o igual que 64 caracteres")]
    public required string Name { get; set; }
}