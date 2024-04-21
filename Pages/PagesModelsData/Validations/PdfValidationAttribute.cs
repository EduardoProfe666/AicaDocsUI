using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Pages.PagesModelsData.Validations;

public class PdfValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            if (file.ContentType!="application/pdf")
            {
                return new ValidationResult(ErrorMessage ?? "El fichero debe ser un .pdf");
            }
        }
        return ValidationResult.Success;
    }
}