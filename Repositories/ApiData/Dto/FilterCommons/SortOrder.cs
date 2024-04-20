using System.ComponentModel;

namespace AicaDocsApi.Dto.FilterCommons;

public enum SortOrder : short
{
    [Description("Ascendente")]
    Asc,
    [Description("Descendente")]
    Desc
}