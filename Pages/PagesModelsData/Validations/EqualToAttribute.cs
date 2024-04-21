using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Pages.PagesModelsData.Validations;

public class  EqualToAttribute : ValidationAttribute
{
    private readonly string _otherPropertyName;

    public EqualToAttribute(string otherPropertyName)
    {
        _otherPropertyName = otherPropertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var otherProperty = validationContext.ObjectType.GetProperty(_otherPropertyName);
        if (otherProperty == null)
        {
            return new ValidationResult($"Property {_otherPropertyName} not found.");
        }

        var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance);
        if (!object.Equals(value, otherPropertyValue))
        {
            return new ValidationResult(ErrorMessage ?? $"La repetición de la contraseña debe ser igual a la nueva contraseña.");
        }

        return ValidationResult.Success;
    }
}