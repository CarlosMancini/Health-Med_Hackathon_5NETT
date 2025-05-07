using System.ComponentModel.DataAnnotations;

namespace Core.Inputs.Cadastrar
{
    public class CadastrarAgendamentoInput
    {
        [Required(ErrorMessage = "É obrigatório informar o paciente")]
        public required int PacienteId { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o médico")]
        public required int MedicoId { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a especialidade médica")]
        public required int EspecialidadeId { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a data e hora do agendamento")]
        [DataType(DataType.DateTime)]
        public required DateTime DataHora { get; set; }
    }
}
