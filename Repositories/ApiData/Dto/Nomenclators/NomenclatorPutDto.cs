using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;

public class NomenclatorPutDto
{
    public required string Name { get; set; }
}