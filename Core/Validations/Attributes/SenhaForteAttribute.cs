using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Core.Validations.Attributes
{
    public class SenhaForteAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is not string senha)
                return false;

            if (senha.Length < 10)
                return false;

            if (!Regex.IsMatch(senha, @"[A-Z]")) // letra maiúscula
                return false;

            if (!Regex.IsMatch(senha, @"[a-z]")) // letra minúscula
                return false;

            if (!Regex.IsMatch(senha, @"\d")) // número
                return false;

            if (!Regex.IsMatch(senha, @"[\W_]")) // caractere especial
                return false;

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return "A senha deve conter no mínimo 10 caracteres, com pelo menos uma letra maiúscula e um caractere especial.";
        }
    }
}
