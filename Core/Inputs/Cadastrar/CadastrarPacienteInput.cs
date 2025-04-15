using System.ComponentModel.DataAnnotations;

namespace Core.Inputs.AdicionarUsuario
{
    public class CadastrarPacienteInput : CadastrarUsuarioInput
    {
        [Required(ErrorMessage = "É obrigatório informar a data de nascimento.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o telefone de contato.")]
        [RegularExpression(@"^\d{2}\d{8,9}$", ErrorMessage = "Formato de telefone inválido.")]
        public required string Telefone { get; set; }

        public string? Observacao { get; set; }
    }
}
