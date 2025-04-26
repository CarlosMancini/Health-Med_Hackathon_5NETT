using Core.Entities;
using Core.Inputs.AdicionarUsuario;
using Core.Inputs.Atualizar;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Utils.Enums;

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
            Usuario usuario = await _usuarioService.CadastrarUsuario(input, PerfilEnum.Paciente);

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

        public async Task Excluir(int usuarioId)
        {
            await _pacienteRepository.ExcluirPaciente(usuarioId);
        }
    }
}
