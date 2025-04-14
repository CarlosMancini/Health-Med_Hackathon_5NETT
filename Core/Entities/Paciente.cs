namespace Core.Entities
{
    public class Paciente : EntityBase
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
