using Core.Entities;

public class HorarioDisponivel : EntityBase
{
    public int MedicoId { get; set; }
    public DayOfWeek HorarioDisponivelDiaSemana { get; set; }
    public TimeSpan HorarioDisponivelHoraInicio { get; set; }
    public TimeSpan HorarioDisponivelHoraFim { get; set; }

    public Medico Medico { get; set; }
}
