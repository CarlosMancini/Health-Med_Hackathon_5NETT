namespace Core.Entities
{
    public class Usuario : EntityBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int PerfilId { get; set; }
        public DateTime CriadoEm { get; set; }

        public Perfil Perfil { get; set; }
    }
}
