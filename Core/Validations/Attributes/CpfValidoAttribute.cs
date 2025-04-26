using System.ComponentModel.DataAnnotations;

namespace Core.Validations.Attributes
{
    public class CpfValidoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string cpf)
                return new ValidationResult("O CPF é obrigatório.");

            cpf = cpf.Trim();

            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
                return new ValidationResult("O CPF deve conter exatamente 11 dígitos numéricos.");

            return ValidationResult.Success!;
        }
    }
}
