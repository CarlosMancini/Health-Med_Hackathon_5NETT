namespace Core.Entities
{
    public class MedicoEspecialidade
    {
        public int Id { get; set; }
        public int MedicoId { get; set; }
        public int EspecialidadeId { get; set; }

        public Medico Medico { get; set; }
        public Especialidade Especialidade { get; set; }
    }
}
