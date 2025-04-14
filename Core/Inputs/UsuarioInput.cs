using Core.Validations.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Core.Inputs
{
    public class UsuarioInput
    {
        [Required(ErrorMessage = "É obrigatório informar o nome do usuário.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o e-mail do usuário.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a senha do usuário")]
        [SenhaForte]
        public string Senha { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o perfil do usuário")]
        public int PerfilId { get; set; }
    }
}
