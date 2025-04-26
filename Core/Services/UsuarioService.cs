using Core.Entities;
using Core.Inputs.AdicionarUsuario;
using Core.Inputs.Atualizar;
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

        public async Task<Usuario> CadastrarUsuario(CadastrarUsuarioInput input, PerfilEnum perfil)
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
                PerfilId = (int)perfil,
                CriadoEm = DateTime.Now,
            };

            return await _usuarioRepository.CadastrarUsuario(usuario);
        }

        public async Task Atualizar(AtualizarUsuarioInput input)
        {
            // TO DO: Validar e-mail e senha atuais estão corretos

            var usuario = await _usuarioRepository.ObterPorEmail(input.EmailAtual) 
                ?? throw new Exception("Usuário não encontrado");

            usuario.UsuarioEmail = input.EmailNovo;
            usuario.UsuarioSenha = _criptografiaService.Criptografar(input.SenhaNova);

            await _usuarioRepository.Atualizar(usuario);
        }
    }
}
