using System.ComponentModel;

namespace AicaDocsUI.Repositories.ApiData.Dto.FilterCommons;

public enum SortOrder : short
{
    [Description("Ascendente")]
    Asc,
    [Description("Descendente")]
    Desc
}