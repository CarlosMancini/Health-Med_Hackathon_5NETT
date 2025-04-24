using Core.Inputs.Compartilhados;

namespace Core.Inputs.Atualizar
{
    public class AtualizarMedicoInput
    {
        public int Id { get; set; }
        public string? CRM { get; set; }

        public ICollection<int> EspecialidadesId { get; set; }
        public ICollection<HorarioDisponivelInput> HorariosDisponiveis { get; set; }
    }
}
