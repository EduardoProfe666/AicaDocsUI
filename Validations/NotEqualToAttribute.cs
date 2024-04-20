using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Validations;

public class NotEqualToAttribute : ValidationAttribute
{
    private readonly string _otherPropertyName;

    public NotEqualToAttribute(string otherPropertyName)
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
        if (object.Equals(value, otherPropertyValue))
        {
            return new ValidationResult(ErrorMessage ?? $"La nueva contraseña no puede ser igual a la antigua.");
        }

        return ValidationResult.Success;
    }
}