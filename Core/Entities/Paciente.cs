namespace Core.Entities
{
    public class Paciente
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
