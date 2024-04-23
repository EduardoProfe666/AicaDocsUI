using AicaDocsUI.Repositories.ApiData.Dto.Commons;

namespace AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;

public class NomenclatorDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required TypeOfNomenclator Type { get; set; }
}