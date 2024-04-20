using System.ComponentModel;

namespace AicaDocsApi.Dto.Downloads.Filter;

public enum SortByDownload : short
{
    [Description("Id")]
    Id,
    [Description("Fecha de descarga")]
    DateDownload,
    [Description("Formato")]
    Format,
    [Description("Usuario")]
    Username
}