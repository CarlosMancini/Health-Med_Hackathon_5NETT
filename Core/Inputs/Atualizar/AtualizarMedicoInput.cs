using Core.Inputs.Compartilhados;
using System.ComponentModel.DataAnnotations;

namespace Core.Inputs.Atualizar
{
    public class AtualizarMedicoInput
    {
        [Required(ErrorMessage = "É obrigatório informar o Id")]
        public required int Id { get; set; }

        public string? CRM { get; set; }
        public decimal? ValorConsulta { get; set; }


        public ICollection<int> EspecialidadesId { get; set; }
        public ICollection<HorarioDisponivelInput> HorariosDisponiveis { get; set; }
    }
}
