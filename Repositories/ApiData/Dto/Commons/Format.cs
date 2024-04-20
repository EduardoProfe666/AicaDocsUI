using System.ComponentModel;

namespace AicaDocsUI.Repositories.ApiData.Dto.Commons;

public enum Format : short
{
    [Description("Pdf")]
    Pdf,
    [Description("Word")]
    Word
}