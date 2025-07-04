﻿namespace Core.Entities
{
    public class Agendamento : EntityBase
    {
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public DateTime AgendamentoDataHora { get; set; }
        public int EspecialidadeId { get; set; }
        public int AgendamentoStatusId { get; set; }
        public int? MotivoCancelamentoId { get; set; }
        public decimal AgendamentoValor { get; set; }

        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public AgendamentoStatus AgendamentoStatus { get; set; }
        public MotivoCancelamento MotivoCancelamento { get; set; }
        public Especialidade Especialidade { get; set; }
    }
}
