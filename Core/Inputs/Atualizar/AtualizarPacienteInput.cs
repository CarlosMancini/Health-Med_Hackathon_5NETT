using System.ComponentModel.DataAnnotations;

namespace Core.Inputs.Atualizar
{
    public class AtualizarPacienteInput
    {
        public required int Id { get; set; }
        public DateTime DataNascimento { get; set; }

        [RegularExpression(@"^\d{2}\d{8,9}$", ErrorMessage = "Formato de telefone inválido.")]
        public string Telefone { get; set; }

        public string? Observacao { get; set; }
    }
}
