using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Pages.PagesModelsData.Validations;

public class FileSizeValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            if (file.Length > 20 * 1024 * 1024) 
            {
                return new ValidationResult(ErrorMessage ?? "El fichero debe tener un tamaño menor o igual que 20 Mb");
            }
        }
        return ValidationResult.Success;
    }   
}