using System.ComponentModel;

namespace AicaDocsUI.Repositories.ApiData.Dto.Commons;

public enum TypeOfNomenclator : short
{
    [Description("Proceso de Documento")]
    ProcessOfDocument,
    [Description("Razón de Descarga")]
    ReasonOfDownload,
    [Description("Alcance de Documento")]
    ScopeOfDocument,
    [Description("Tipo de Documento")]
    TypeOfDocument
    
}