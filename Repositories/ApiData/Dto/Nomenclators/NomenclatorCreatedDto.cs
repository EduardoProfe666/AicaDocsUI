using System.ComponentModel.DataAnnotations;
using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using Microsoft.Extensions.Options;

namespace AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;

public class NomenclatorCreatedDto
{
    public required string Name { get; set; }

    public required TypeOfNomenclator Type { get; set; }
}