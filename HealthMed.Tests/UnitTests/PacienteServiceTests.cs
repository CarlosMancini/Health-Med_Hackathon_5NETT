using Core.Entities;
using Core.Inputs.AdicionarUsuario;
using Core.Inputs.Atualizar;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Core.Utils.Enums;
using Moq;

namespace HealthMed.Tests.UnitTests
{
    public class PacienteServiceTests
    {
        private readonly Mock<IPacienteRepository> _mockPacienteRepository;
        private readonly Mock<IUsuarioService> _mockUsuarioService;
        private readonly PacienteService _pacienteService;

        public PacienteServiceTests()
        {
            _mockPacienteRepository = new Mock<IPacienteRepository>();
            _mockUsuarioService = new Mock<IUsuarioService>();
            _pacienteService = new PacienteService(_mockPacienteRepository.Object, _mockUsuarioService.Object);
        }

        [Fact]
        public async Task Cadastrar_DeveChamarUsuarioServiceECadastrarNoRepositorio()
        {
            // Arrange
            var mockPacienteRepo = new Mock<IPacienteRepository>();
            var mockUsuarioService = new Mock<IUsuarioService>();
            var mockCriptografiaService = new Mock<ICriptografiaService>();

            var input = new CadastrarPacienteInput
            {
                Nome = "Dr. João",
                UsuarioCPF = "01234567891",
                Email = "joao@teste.com",
                Senha = "123456",
                DataNascimento = new DateTime(1998, 05, 05),
                Telefone = "61956845683"
            };

            string senhaCriptografada = string.Empty;

            mockCriptografiaService
                .Setup(c => c.Criptografar(input.Senha))
                .Returns(senhaCriptografada);

            var usuarioCriado = new Usuario
            {
                Id = 42,
                UsuarioCPF = input.UsuarioCPF,
                UsuarioEmail = input.Email,
                UsuarioSenha = senhaCriptografada,
            };

            _mockUsuarioService
                .Setup(u => u.CadastrarUsuario(It.IsAny<CadastrarUsuarioInput>(), PerfilEnum.Paciente))
                .ReturnsAsync(usuarioCriado);

            // Act
            await _pacienteService.Cadastrar(input);

            // Assert
            _mockUsuarioService.Verify(u => u.CadastrarUsuario(It.IsAny<CadastrarUsuarioInput>(), PerfilEnum.Paciente), Times.Once);
            _mockPacienteRepository.Verify(r => r.Cadastrar(It.Is<Paciente>(m =>
                m.Id == usuarioCriado.Id &&
                m.PacienteDataNascimento == input.DataNascimento &&
                m.PacienteTelefone == input.Telefone
            )), Times.Once);
        }

        [Fact]
        public async Task Atualizar_DeveChamarRepositorioComDadosAtualizados()
        {
            var input = new AtualizarPacienteInput
            {
                Id = 12,
                DataNascimento = new DateTime(1985, 8, 10),
                Telefone = "11888887777",
                Observacao = "Histórico de alergias"
            };

            await _pacienteService.Atualizar(input);

            _mockPacienteRepository.Verify(r => r.Atualizar(It.Is<Paciente>(p =>
                p.Id == input.Id &&
                p.PacienteDataNascimento == input.DataNascimento &&
                p.PacienteTelefone == input.Telefone &&
                p.PacienteObservacao == input.Observacao
            )), Times.Once);
        }

        [Fact]
        public async Task Excluir_DeveChamarRepositorioComIdCorreto()
        {
            int usuarioId = 99;

            await _pacienteService.Excluir(usuarioId);

            _mockPacienteRepository.Verify(r => r.ExcluirPaciente(usuarioId), Times.Once);
        }
    }
}
