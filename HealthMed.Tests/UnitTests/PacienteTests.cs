using Core.Entities;
using Core.Inputs.AdicionarUsuario;
using Core.Inputs.Compartilhados;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Core.Utils.Enums;
using Moq;

namespace HealthMed.Tests.UnitTests
{
    public class PacienteTests
    {
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

            mockUsuarioService
                .Setup(u => u.CadastrarUsuario(It.IsAny<CadastrarUsuarioInput>(), PerfilEnum.Paciente))
                .ReturnsAsync(usuarioCriado);

            var service = new PacienteService(mockPacienteRepo.Object, mockUsuarioService.Object);

            // Act
            await service.Cadastrar(input);

            // Assert
            mockUsuarioService.Verify(u => u.CadastrarUsuario(It.IsAny<CadastrarUsuarioInput>(), PerfilEnum.Paciente), Times.Once);
            mockPacienteRepo.Verify(r => r.Cadastrar(It.Is<Paciente>(m =>
                m.Id == usuarioCriado.Id &&
                m.PacienteDataNascimento == input.DataNascimento &&
                m.PacienteTelefone == input.Telefone
            )), Times.Once);
        }
    }
}
