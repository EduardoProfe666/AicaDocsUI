using System.ComponentModel;

namespace AicaDocsUI.DataModels;

public enum Format : short
{
    [Description("Pdf")]
    Pdf,
    [Description("Word")]
    Word
}