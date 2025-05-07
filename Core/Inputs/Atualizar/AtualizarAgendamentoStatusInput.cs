using System.ComponentModel.DataAnnotations;

namespace Core.Inputs.Atualizar
{
    public class AtualizarAgendamentoStatusInput
    {
        [Required(ErrorMessage = "É obrigatório informar o id do cancelamento")]
        public required int AgendamentoId { get; set; }
        [Required(ErrorMessage = "É obrigatório informar o status")]
        public required int StatusId { get; set; }
        public int? MotivoCancelamentoId { get; set; }
    }
}
