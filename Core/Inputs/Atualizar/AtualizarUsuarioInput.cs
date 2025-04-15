using Core.Validations.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Core.Inputs.Atualizar
{
    public class AtualizarUsuarioInput
    {
        [Required(ErrorMessage = "É obrigatório informar o Id do usuário")]
        public required int Id { get; set; }

        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public string? Email { get; set; }

        [SenhaForte]
        public string? Senha { get; set; }
    }
}
