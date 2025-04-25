using Core.Entities;
using Core.Inputs.AdicionarUsuario;
using Core.Inputs.Atualizar;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Core.Services
{
    public class PacienteService : ServiceBase<Paciente>, IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IUsuarioService _usuarioService;

        public PacienteService(IPacienteRepository pacienteRepository, IUsuarioService usuarioService) : base(pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
            _usuarioService = usuarioService;
        }

        public async Task Cadastrar(CadastrarPacienteInput input)
        {
            Usuario usuario = await _usuarioService.CadastrarUsuario(input);

            var paciente = new Paciente
            {
                Id = usuario.Id,
                PacienteDataNascimento = input.DataNascimento,
                PacienteTelefone = input.Telefone,
                PacienteObservacao = input.Observacao
            };

            await _pacienteRepository.Cadastrar(paciente);
        }

        public async Task Atualizar(AtualizarPacienteInput input)
        {
            Paciente paciente = new Paciente
            {
                Id = input.Id,
                PacienteDataNascimento = input.DataNascimento,
                PacienteTelefone = input.Telefone,
                PacienteObservacao = input.Observacao
            };

            await _pacienteRepository.Atualizar(paciente);
        }
    }
}
