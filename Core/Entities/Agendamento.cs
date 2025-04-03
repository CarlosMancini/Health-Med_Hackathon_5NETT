namespace Core.Entities
{
    public class Agendamento
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public int HorarioId { get; set; }
        public int StatusId { get; set; }
        public int? MotivoCancelamentoId { get; set; }

        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public HorarioDisponivel Horario { get; set; }
        public AgendamentoStatus Status { get; set; }
        public MotivoCancelamento MotivoCancelamento { get; set; }
    }
}
