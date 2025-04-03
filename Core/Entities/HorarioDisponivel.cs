namespace Core.Entities
{
    public class HorarioDisponivel
    {
        public int Id { get; set; }
        public int MedicoId { get; set; }
        public DateTime DataHora { get; set; }

        public Medico Medico { get; set; }
    }
}
