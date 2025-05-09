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
    public class MedicoTests
    {
        [Fact]
        public async Task Cadastrar_DeveChamarUsuarioServiceECadastrarNoRepositorio()
        {
            // Arrange
            var mockMedicoRepo = new Mock<IMedicoRepository>();
            var mockUsuarioService = new Mock<IUsuarioService>();
            var mockCriptografiaService = new Mock<ICriptografiaService>();

            var input = new CadastrarMedicoInput
            {
                Nome = "Dr. João",
                UsuarioCPF = "01234567891",
                Email = "joao@teste.com",
                Senha = "123456",
                CRM = "12345",
                ValorConsulta = 300.00m,
                EspecialidadesId = new List<int> { 1, 2 },

                HorariosDisponiveis = new List<HorarioDisponivelInput> 
                { 
                    new HorarioDisponivelInput 
                    { 
                        DiaSemana = 0, 
                        HoraInicio = new TimeSpan(8,0,0), 
                        HoraFim = new TimeSpan(12,0,0) 
                    } 
                }
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
                .Setup(u => u.CadastrarUsuario(It.IsAny<CadastrarUsuarioInput>(), PerfilEnum.Medico))
                .ReturnsAsync(usuarioCriado);

            var service = new MedicoService(mockMedicoRepo.Object, mockUsuarioService.Object);

            // Act
            await service.Cadastrar(input);

            // Assert
            mockUsuarioService.Verify(u => u.CadastrarUsuario(It.IsAny<CadastrarUsuarioInput>(), PerfilEnum.Medico), Times.Once);
            mockMedicoRepo.Verify(r => r.Cadastrar(It.Is<Medico>(m =>
                m.Id == usuarioCriado.Id &&
                m.MedicoCRM == input.CRM &&
                m.MedicoValorConsulta == input.ValorConsulta &&
                m.MedicoEspecialidades.Count == input.EspecialidadesId.Count
            )), Times.Once);
        }
    }
}