using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Core.Services
{
    public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository) : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task Cadastrar(Usuario usuario)
        {
            // TO DO: add validação de e-mail duplicado

            await _usuarioRepository.Cadastrar(usuario);
        }

        public async Task<Usuario?> Autenticar(string email, string senha)
        {
            var usuario = await _usuarioRepository.Autenticar(email, senha);

            if (usuario == null || usuario.UsuarioSenha != senha)
                return null;

            return usuario;
        }
    }
}
