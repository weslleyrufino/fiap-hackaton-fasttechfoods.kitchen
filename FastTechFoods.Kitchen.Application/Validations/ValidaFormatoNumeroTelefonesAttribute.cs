using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FastTechFoods.Kitchen.Application.Validations;
public class ValidaFormatoNumeroTelefonesAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var telefone = value as string;
        if (!string.IsNullOrEmpty(telefone) && Regex.IsMatch(telefone, @"^(?:[2-5]|9[1-9])[0-9]{3}-?[0-9]{4}$"))
        {
            return ValidationResult.Success;
        }
        return new ValidationResult(ErrorMessage ?? "Formato de telefone inválido.");
    }
}
