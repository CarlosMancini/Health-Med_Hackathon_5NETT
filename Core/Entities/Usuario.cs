namespace Core.Entities
{
    public class Usuario : EntityBase
    {
        public string UsuarioNome { get; set; }
        public string UsuarioEmail { get; set; }
        public string UsuarioCPF { get; set; }
        public string UsuarioSenha { get; set; }
        public DateTime CriadoEm { get; set; }
        public int PerfilId { get; set; }

        public Perfil Perfil { get; set; }
    }
}
