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
        private readonly IMedicoService _medicoService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPacienteRepository _pacienteRepository;

        public UsuarioService(
            ICriptografiaService criptografiaService,
            IMedicoService medicoService,
            IUsuarioRepository usuarioRepository,
            IMedicoRepository medicoRepository,
            IPacienteRepository pacienteRepository
        ) : base(usuarioRepository)
        {
            _criptografiaService = criptografiaService;
            _medicoService = medicoService;
            _usuarioRepository = usuarioRepository;
            _pacienteRepository = pacienteRepository;
        }

        public async Task<Usuario?> Autenticar(string email, string senha)
        {
            var usuario = await _usuarioRepository.Autenticar(email, senha);

            if (usuario == null || usuario.UsuarioSenha != senha)
                return null;

            return usuario;
        }

        public async Task CadastrarMedico(CadastrarMedicoInput input)
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

            await _usuarioRepository.Cadastrar(usuario);
            await _medicoService.Cadastrar(usuario.Id, input);
        }

        public async Task CadastrarPaciente(CadastrarPacienteInput input)
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
                PerfilId = (int)PerfilEnum.Paciente,
                CriadoEm = DateTime.Now,
            };

            await _usuarioRepository.Cadastrar(usuario);

            var paciente = new Paciente
            {
                Id = usuario.Id,
                PacienteDataNascimento = input.DataNascimento,
                PacienteTelefone = input.Telefone,
                PacienteObservacao = input.Observacao
            };

            await _pacienteRepository.Cadastrar(paciente);
        }
    }
}
