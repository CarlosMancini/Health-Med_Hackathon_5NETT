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
        private readonly IMedicoRepository _medicoRepository;
        private readonly IPacienteRepository _pacienteRepository;

        public UsuarioService(
            ICriptografiaService criptografiaService,
            IUsuarioRepository usuarioRepository,
            IMedicoRepository medicoRepository,
            IPacienteRepository pacienteRepository
        ) : base(usuarioRepository)
        {
            _criptografiaService = criptografiaService;
            _usuarioRepository = usuarioRepository;
            _medicoRepository = medicoRepository;
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

            var medico = new Medico
            {
                Id = usuario.Id,
                MedicoCRM = input.CRM
            };

            await _medicoRepository.Cadastrar(medico);
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
                PerfilId = (int)PerfilEnum.Medico,
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

        public async Task AtualizarMedico(AtualizarMedicoInput input)
        {
            Usuario usuarioBanco = await _usuarioRepository.ObterPorId(input.Id);

            Usuario usuario = new Usuario
            {
                Id = input.Id,
                UsuarioNome = usuarioBanco.UsuarioNome,
                UsuarioCPF = usuarioBanco.UsuarioCPF,
                UsuarioEmail = input.Email,
                UsuarioSenha = _criptografiaService.Criptografar(input.Senha),
                PerfilId = (int)PerfilEnum.Medico,
                CriadoEm = usuarioBanco.CriadoEm,
            };

            await _usuarioRepository.Atualizar(usuario);

            Medico medico = new Medico
            {
                Id = input.Id,
                MedicoCRM = input.CRM,
            };

            await _medicoRepository.Atualizar(medico);
        }
    }
}
