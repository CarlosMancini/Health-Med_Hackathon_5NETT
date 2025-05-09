using System.ComponentModel.DataAnnotations;

namespace Core.Inputs.Compartilhados
{
    public class HorarioDisponivelInput
    {
        [Required(ErrorMessage = "É obrigatório informar o dia da semana disponível")]
        public DayOfWeek DiaSemana { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o horário de início disponível")]
        [DataType(DataType.Time)]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o horário de fim disponível")]
        [DataType(DataType.Time)]
        public TimeSpan HoraFim { get; set; }
    }
}
