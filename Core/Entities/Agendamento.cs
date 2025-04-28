namespace Core.Entities
{
    public class Agendamento : EntityBase
    {
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public int HorarioId { get; set; }
        public int StatusId { get; set; }
        public int? MotivoCancelamentoId { get; set; }

        // TO DO: Lógica de visualização de agenda do médico e add propriedade de valor da consulta

        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public HorarioDisponivel Horario { get; set; }
        public AgendamentoStatus Status { get; set; }
        public MotivoCancelamento MotivoCancelamento { get; set; }
    }
}
