using Core.Entities;
using Core.Inputs.Atualizar;
using Core.Inputs.Cadastrar;
using Core.Inputs.Pesquisar;
using Core.Interfaces.Repositories;
using Core.Services;
using Core.Utils.Enums;
using Moq;

namespace HealthMed.Tests.UnitTests
{
    public class AgendamentoServiceTests
    {
        private readonly Mock<IAgendamentoRepository> _mockAgendamentoRepository;
        private readonly AgendamentoService _agendamentoService;

        public AgendamentoServiceTests()
        {
            _mockAgendamentoRepository = new Mock<IAgendamentoRepository>();
            _agendamentoService = new AgendamentoService(_mockAgendamentoRepository.Object);
        }

        [Fact]
        public async Task Pesquisar_DeveChamarRepositorioComInputCorreto()
        {
            var input = new FiltroPesquisaAgendamentoInput();
            var lista = new List<Agendamento>();
            _mockAgendamentoRepository.Setup(r => r.Pesquisar(input)).ReturnsAsync(lista);

            var resultado = await _agendamentoService.Pesquisar(input);

            Assert.Equal(lista, resultado);
            _mockAgendamentoRepository.Verify(r => r.Pesquisar(input), Times.Once);
        }

        [Fact]
        public async Task AgendarConsulta_DeveChamarRepositorioComInputCorreto()
        {
            var input = new CadastrarAgendamentoInput
            {
                PacienteId = 1,
                MedicoId = 5,
                EspecialidadeId = (int)EspecialidadeEnum.Pediatria,
                DataHora = new DateTime(2025, 02, 28, 8, 30, 0)
            };

            await _agendamentoService.AgendarConsulta(input);

            _mockAgendamentoRepository.Verify(r => r.AgendarConsulta(input), Times.Once);
        }

        [Fact]
        public async Task AtualizarStatus_DeveAtualizarStatusECancelamento_QuandoStatusForCancelado()
        {
            var input = new AtualizarAgendamentoStatusInput
            {
                AgendamentoId = 1,
                StatusId = (int)AgendamentoStatusEnum.Cancelado,
                MotivoCancelamentoId = 99
            };
            var agendamento = new Agendamento { Id = 1 };
            _mockAgendamentoRepository.Setup(r => r.ObterPorId(input.AgendamentoId)).ReturnsAsync(agendamento);

            await _agendamentoService.AtualizarStatus(input);

            Assert.Equal(input.StatusId, agendamento.AgendamentoStatusId);
            Assert.Equal(input.MotivoCancelamentoId, agendamento.MotivoCancelamentoId);
            _mockAgendamentoRepository.Verify(r => r.Atualizar(agendamento), Times.Once);
        }

        [Fact]
        public async Task AtualizarStatus_DeveAtualizarStatusESemMotivo_QuandoStatusNaoForCancelado()
        {
            var input = new AtualizarAgendamentoStatusInput
            {
                AgendamentoId = 1,
                StatusId = (int)AgendamentoStatusEnum.Agendado,
                MotivoCancelamentoId = 99
            };
            var agendamento = new Agendamento { Id = 1 };
            _mockAgendamentoRepository.Setup(r => r.ObterPorId(input.AgendamentoId)).ReturnsAsync(agendamento);

            await _agendamentoService.AtualizarStatus(input);

            Assert.Equal(input.StatusId, agendamento.AgendamentoStatusId);
            Assert.Null(agendamento.MotivoCancelamentoId);
            _mockAgendamentoRepository.Verify(r => r.Atualizar(agendamento), Times.Once);
        }
    }
}
