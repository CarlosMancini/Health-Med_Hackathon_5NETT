using Core.Validations.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Core.Inputs.AdicionarUsuario
{
    public class CadastrarUsuarioInput
    {
        [Required(ErrorMessage = "É obrigatório informar o nome do usuário.")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o e-mail do usuário.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o CPF do usuário.")]
        [CpfValido]
        public required string UsuarioCPF { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a senha do usuário")]
        [SenhaForte]
        public required string Senha { get; set; }
    }
}
