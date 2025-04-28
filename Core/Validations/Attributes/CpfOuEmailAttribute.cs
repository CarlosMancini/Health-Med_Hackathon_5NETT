using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Core.Validations.Attributes
{
    public class CpfOuEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null || string.IsNullOrWhiteSpace(value.ToString()))
                return new ValidationResult("O login (CPF ou Email) é obrigatório.");

            var login = value.ToString()!;

            var cpfRegex = new Regex(@"^\d{11}$");
            var emailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");

            if (cpfRegex.IsMatch(login) || emailRegex.IsMatch(login))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("O login informado deve ser um CPF válido (11 dígitos) ou um Email válido.");
        }
    }
}
