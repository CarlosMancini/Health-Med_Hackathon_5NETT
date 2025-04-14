namespace Core.Entities
{
    public class Medico : EntityBase
    {
        public string CRM { get; set; }

        public Usuario Usuario { get; set; }
        public ICollection<MedicoEspecialidade> MedicoEspecialidades { get; set; }
    }
}
