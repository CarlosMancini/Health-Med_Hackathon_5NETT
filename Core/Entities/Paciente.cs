namespace Core.Entities
{
    public class Paciente : EntityBase
    {
        public DateTime PacienteDataNascimento { get; set; }
        public string PacienteTelefone { get; set; }
        public string? PacienteObservacao { get; set; }

        public Usuario Usuario { get; set; }
    }
}
