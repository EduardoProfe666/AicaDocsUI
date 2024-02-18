using System.ComponentModel;

namespace AicaDocsUI.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var type = value.GetType();
        var memInfo = type.GetMember(value.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length >  0 ? ((DescriptionAttribute)attributes[0]).Description : value.ToString();
    }   
}