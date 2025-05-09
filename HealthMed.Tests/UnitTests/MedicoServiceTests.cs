using Core.DTOs;
using Core.Entities;
using Core.Inputs.AdicionarUsuario;
using Core.Inputs.Atualizar;
using Core.Inputs.Compartilhados;
using Core.Inputs.Pesquisar;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Core.Utils.Enums;
using Moq;

namespace HealthMed.Tests.UnitTests
{
    public class MedicoServiceTests
    {
        private readonly Mock<IMedicoRepository> _medicoRepoMock = new();
        private readonly Mock<IUsuarioService> _usuarioServiceMock = new();
        private readonly MedicoService _medicoService;

        public MedicoServiceTests()
        {
            _medicoService = new MedicoService(_medicoRepoMock.Object, _usuarioServiceMock.Object);
        }

        [Fact]
        public async Task ObterMedicoPorId_DeveRetornarMedico()
        {
            var medico = new Medico { Id = 1, MedicoCRM = "1234" };
            _medicoRepoMock.Setup(r => r.ObterMedicoPorId(1)).ReturnsAsync(medico);

            var result = await _medicoService.ObterMedicoPorId(1);

            Assert.Equal(medico, result);
        }

        [Fact]
        public async Task ObterPorEspecialidade_DeveRetornarListaDeMedicos()
        {
            var lista = new List<Medico> { new Medico { Id = 1 }, new Medico { Id = 2 } };
            _medicoRepoMock.Setup(r => r.ObterPorEspecialidade(99)).ReturnsAsync(lista);

            var result = await _medicoService.ObterPorEspecialidade(99);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task PesquisarMedicosDisponiveis_DeveRetornarLista()
        {
            var filtro = new FiltroPesquisaMedicoDisponivelInput();
            var disponiveis = new List<MedicoDisponivelDto> { new MedicoDisponivelDto(), new MedicoDisponivelDto() };
            _medicoRepoMock.Setup(r => r.PesquisarMedicosDisponiveis(filtro)).ReturnsAsync(disponiveis);

            var result = await _medicoService.PesquisarMedicosDisponiveis(filtro);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Cadastrar_DeveChamarUsuarioServiceECadastrarNoRepositorio()
        {
            // Arrange
            var mockCriptografiaService = new Mock<ICriptografiaService>();

            var input = new CadastrarMedicoInput
            {
                Nome = "Dr. João",
                UsuarioCPF = "01234567891",
                Email = "joao@teste.com",
                Senha = "123456",
                CRM = "12345",
                ValorConsulta = 300.00m,
                EspecialidadesId = new List<int> { (int)EspecialidadeEnum.Cardiologia, (int)EspecialidadeEnum.Psiquiatria },

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

            _usuarioServiceMock
                .Setup(u => u.CadastrarUsuario(It.IsAny<CadastrarUsuarioInput>(), PerfilEnum.Medico))
                .ReturnsAsync(usuarioCriado);

            var service = new MedicoService(_medicoRepoMock.Object, _usuarioServiceMock.Object);

            // Act
            await service.Cadastrar(input);

            // Assert
            _usuarioServiceMock.Verify(u => u.CadastrarUsuario(It.IsAny<CadastrarUsuarioInput>(), PerfilEnum.Medico), Times.Once);
            _medicoRepoMock.Verify(r => r.Cadastrar(It.Is<Medico>(m =>
                m.Id == usuarioCriado.Id &&
                m.MedicoCRM == input.CRM &&
                m.MedicoValorConsulta == input.ValorConsulta &&
                m.MedicoEspecialidades.Count == input.EspecialidadesId.Count
            )), Times.Once);
        }

        [Fact]
        public async Task Atualizar_DeveAtualizarMedico()
        {
            var input = new AtualizarMedicoInput
            {
                Id = 1,
                CRM = "5678",
                ValorConsulta = 300,
                EspecialidadesId = new List<int> { 1, 2 },
                HorariosDisponiveis = new List<HorarioDisponivelInput>
            {
                new HorarioDisponivelInput { DiaSemana = DayOfWeek.Monday, HoraInicio = new TimeSpan(8, 0, 0), HoraFim = new TimeSpan(12, 0, 0) }
            }
            };

            var medicoExistente = new Medico
            {
                Id = 1,
                MedicoCRM = "1234",
                MedicoValorConsulta = 200,
                MedicoEspecialidades = new List<MedicoEspecialidade>(),
                HorariosDisponiveis = new List<HorarioDisponivel>()
            };

            _medicoRepoMock.Setup(r => r.ObterMedicoPorId(1)).ReturnsAsync(medicoExistente);

            await _medicoService.Atualizar(input);

            _medicoRepoMock.Verify(r => r.Atualizar(It.Is<Medico>(m =>
                m.MedicoCRM == "5678" &&
                m.MedicoValorConsulta == 300 &&
                m.MedicoEspecialidades.Count == 2 &&
                m.HorariosDisponiveis.Any(h => h.HorarioDisponivelHoraInicio == new TimeSpan(8, 0, 0))
            )), Times.Once);
        }

        [Fact]
        public async Task Atualizar_MedicoNaoExiste_DeveLancarExcecao()
        {
            _medicoRepoMock.Setup(r => r.ObterMedicoPorId(99)).ReturnsAsync((Medico)null);

            var input = new AtualizarMedicoInput { Id = 99, EspecialidadesId = new List<int>(), HorariosDisponiveis = new List<HorarioDisponivelInput>() };

            await Assert.ThrowsAsync<Exception>(() => _medicoService.Atualizar(input));
        }

        [Fact]
        public async Task Excluir_DeveChamarExcluirMedico()
        {
            await _medicoService.Excluir(42);

            _medicoRepoMock.Verify(r => r.ExcluirMedico(42), Times.Once);
        }
    }
}