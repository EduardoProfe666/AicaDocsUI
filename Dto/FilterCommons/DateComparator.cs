using System.ComponentModel;

namespace AicaDocsApi.Dto.FilterCommons;

public enum DateComparator : short
{
    [Description("Igual que")]
    Equal,
    [Description("Mayor que")]
    Greater,
    [Description("Mayor o igual que")]
    EqualGreater,
    [Description("Menor que")]
    Less,
    [Description("Menor o igual que")]
    EqualLess
}