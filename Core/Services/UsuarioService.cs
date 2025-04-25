using Core.Entities;
using Core.Inputs.AdicionarUsuario;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Utils.Enums;

namespace Core.Services
{
    public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
    {
        private readonly ICriptografiaService _criptografiaService;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(
            ICriptografiaService criptografiaService,
            IUsuarioRepository usuarioRepository
        ) : base(usuarioRepository)
        {
            _criptografiaService = criptografiaService;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario?> Autenticar(string email, string senha)
        {
            var usuario = await _usuarioRepository.Autenticar(email, senha);

            if (usuario == null || usuario.UsuarioSenha != senha)
                return null;

            return usuario;
        }

        public async Task<Usuario> CadastrarUsuario(CadastrarUsuarioInput input)
        {
            var emailJaExiste = await _usuarioRepository.ObterPorEmail(input.Email);
            if (emailJaExiste != null)
                throw new Exception("E-mail já cadastrado.");

            Usuario usuario = new Usuario
            {
                UsuarioNome = input.Nome,
                UsuarioEmail = input.Email,
                UsuarioCPF = input.UsuarioCPF,
                UsuarioSenha = _criptografiaService.Criptografar(input.Senha),
                PerfilId = (int)PerfilEnum.Medico,
                CriadoEm = DateTime.Now,
            };

            return await _usuarioRepository.CadastrarUsuario(usuario);
        }
    }
}
