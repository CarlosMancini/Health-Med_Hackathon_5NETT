namespace Core.Entities
{
    public class HorarioDisponivel : EntityBase
    {
        public int MedicoId { get; set; }
        public DateTime DataHora { get; set; }

        public Medico Medico { get; set; }
    }
}
