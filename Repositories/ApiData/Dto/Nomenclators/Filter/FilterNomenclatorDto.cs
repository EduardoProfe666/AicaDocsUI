using AicaDocsApi.Dto.FilterCommons;
using AicaDocsApi.Dto.Nomenclators.Filter;
using AicaDocsUI.Models;

namespace AicaDocsUI.Dto.Nomenclators.Filter;

public class FilterNomenclatorDto
{
    public required TypeOfNomenclator? Type { get; set; }

    public SortByNomenclator SortBy { get; set; } = SortByNomenclator.Id;
    public SortOrder SortOrder { get; set; } = SortOrder.Asc;
    public PaginationParams PaginationParams { get; set; } = new PaginationParams();
}