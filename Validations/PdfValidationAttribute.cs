using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Validations;

public class PdfValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            if (file.ContentType!="application/pdf")
            {
                return new ValidationResult("El fichero debe ser un .pdf");
            }
        }
        return ValidationResult.Success;
    }
}