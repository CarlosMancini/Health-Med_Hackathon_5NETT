namespace Core.Entities
{
    public class Medico : EntityBase
    {
        public int UsuarioId { get; set; }
        public string CRM { get; set; }

        public Usuario Usuario { get; set; }
        public ICollection<MedicoEspecialidade> Especialidades { get; set; }
    }
}
