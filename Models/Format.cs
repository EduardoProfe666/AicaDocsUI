using System.ComponentModel;

namespace AicaDocsUI.Models;

public enum Format : short
{
    [Description("Pdf")]
    Pdf,
    [Description("Word")]
    Word
}