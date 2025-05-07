namespace Core.Entities
{
    public class Medico : EntityBase
    {
        public string MedicoCRM { get; set; }
        public decimal MedicoValorConsulta { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<MedicoEspecialidade> MedicoEspecialidades { get; set; }
        public ICollection<HorarioDisponivel> HorariosDisponiveis { get; set; }
    }
}
