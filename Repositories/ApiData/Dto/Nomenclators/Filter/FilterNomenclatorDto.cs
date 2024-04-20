using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.FilterCommons;

namespace AicaDocsUI.Repositories.ApiData.Dto.Nomenclators.Filter;

public class FilterNomenclatorDto
{
    public required TypeOfNomenclator? Type { get; set; }

    public SortByNomenclator SortBy { get; set; } = SortByNomenclator.Id;
    public SortOrder SortOrder { get; set; } = SortOrder.Asc;
    public PaginationParams PaginationParams { get; set; } = new PaginationParams();
}