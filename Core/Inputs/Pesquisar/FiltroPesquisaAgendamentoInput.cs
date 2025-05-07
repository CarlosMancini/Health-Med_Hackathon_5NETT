namespace Core.Inputs.Pesquisar
{
    public class FiltroPesquisaAgendamentoInput
    {
        public int? PacienteId { get; set; }
        public int? MedicoId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? DataHoraInicio { get; set; }
        public DateTime? DataHoraFim { get; set; }
    }
}
