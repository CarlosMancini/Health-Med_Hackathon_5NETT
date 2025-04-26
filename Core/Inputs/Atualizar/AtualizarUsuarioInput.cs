using Core.Validations.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Core.Inputs.Atualizar
{
    public class AtualizarUsuarioInput
    {
        [Required(ErrorMessage = "É obrigatório informar o e-mail atual do usuário.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public required string EmailAtual { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o novo e-mail do usuário.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public required string EmailNovo { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a senha atual do usuário.")]
        public required string SenhaAtual { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a nova senha do usuário.")]
        [SenhaForte]
        public required string SenhaNova { get; set; }
    }
}
