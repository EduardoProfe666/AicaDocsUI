using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Pages.PagesModelsData.Validations;

public class WordValidationAttribute: ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            if (file.ContentType!="application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                return new ValidationResult(ErrorMessage ?? "El fichero debe ser un .docx");
            }
        }
        return ValidationResult.Success;
    }
}