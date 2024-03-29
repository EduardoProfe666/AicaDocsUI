using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Validations;

public class WordValidationAttribute: ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            if (file.ContentType!="application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                return new ValidationResult("El fichero debe ser un .docx");
            }
        }
        return ValidationResult.Success;
    }
}