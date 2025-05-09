using Core.Entities;
using Core.Inputs.AdicionarUsuario;
using Core.Inputs.Atualizar;
using Core.Inputs.Autenticar;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Core.Utils.Enums;
using Moq;

namespace HealthMed.Tests.UnitTests
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepository = new();
        private readonly Mock<ICriptografiaService> _mockCriptografiaService = new();
        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTests()
        {
            _usuarioService = new UsuarioService(_mockCriptografiaService.Object, _mockUsuarioRepository.Object);
        }

        [Fact]
        public async Task AutenticarMedico_DeveRetornarUsuario_QuandoCredenciaisForemCorretas()
        {
            var input = new AutenticacaoMedicoInput 
            { 
                CRM = "123598", 
                Senha = "123456" 
            };

            var usuario = new Usuario { UsuarioSenha = "123456" };

            _mockUsuarioRepository.Setup(r => r.AutenticarMedico(input)).ReturnsAsync(usuario);

            var resultado = await _usuarioService.AutenticarMedico(input);

            Assert.NotNull(resultado);
        }

        [Fact]
        public async Task AutenticarPaciente_DeveRetornarNull_QuandoSenhaEstiverIncorreta()
        {
            var input = new AutenticacaoPacienteInput 
            { 
                Login = "teste@paciente.com", 
                Senha = "errada" 
            };

            var usuario = new Usuario { UsuarioSenha = "correta" };

            _mockUsuarioRepository.Setup(r => r.AutenticarPaciente(input)).ReturnsAsync(usuario);

            var resultado = await _usuarioService.AutenticarPaciente(input);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task CadastrarUsuario_DeveLancarExcecao_SeEmailJaEstiverCadastrado()
        {
            var input = new CadastrarUsuarioInput 
            { 
                Nome = "Bruce Wayne", 
                Email = "ja@existe.com", 
                Senha = "123456", 
                UsuarioCPF = "01234567891" 
            };

            _mockUsuarioRepository.Setup(r => r.ObterPorEmail(input.Email)).ReturnsAsync(new Usuario());

            await Assert.ThrowsAsync<Exception>(() => _usuarioService.CadastrarUsuario(input, PerfilEnum.Medico));
        }

        [Fact]
        public async Task CadastrarUsuario_DeveRetornarUsuario_QuandoCadastroForValido()
        {
            var input = new CadastrarUsuarioInput
            {
                Email = "novo@teste.com",
                Nome = "Novo",
                Senha = "123456",
                UsuarioCPF = "12345678901"
            };

            _mockUsuarioRepository.Setup(r => r.ObterPorEmail(input.Email)).ReturnsAsync((Usuario)null);
            _mockCriptografiaService.Setup(c => c.Criptografar(input.Senha)).Returns("senhaCriptografada");

            _mockUsuarioRepository.Setup(r => r.CadastrarUsuario(It.IsAny<Usuario>()))
                .ReturnsAsync((Usuario u) => u);

            var usuario = await _usuarioService.CadastrarUsuario(input, PerfilEnum.Paciente);

            Assert.Equal("senhaCriptografada", usuario.UsuarioSenha);
            Assert.Equal((int)PerfilEnum.Paciente, usuario.PerfilId);
        }

        [Fact]
        public async Task Atualizar_DeveLancarExcecao_SeUsuarioNaoForEncontrado()
        {
            var input = new AtualizarUsuarioInput 
            { 
                EmailAtual = "inexistente@teste.com", 
                EmailNovo = "emailteste@teste.com", 
                SenhaAtual = "123456", 
                SenhaNova = "1234657" 
            };

            _mockUsuarioRepository.Setup(r => r.ObterPorEmail(input.EmailAtual)).ReturnsAsync((Usuario)null);

            await Assert.ThrowsAsync<Exception>(() => _usuarioService.Atualizar(input, 1));
        }

        [Fact]
        public async Task Atualizar_DeveLancarExcecao_SeUsuarioNaoForProprio()
        {
            var input = new AtualizarUsuarioInput 
            { 
                EmailAtual = "teste@teste.com", 
                EmailNovo = "emailteste@teste.com", 
                SenhaAtual = "123456", 
                SenhaNova = "1234657" 
            };

            var usuario = new Usuario { Id = 2 };

            _mockUsuarioRepository.Setup(r => r.ObterPorEmail(input.EmailAtual)).ReturnsAsync(usuario);

            await Assert.ThrowsAsync<Exception>(() => _usuarioService.Atualizar(input, usuarioLogadoId: 1));
        }

        [Fact]
        public async Task Atualizar_DeveLancarExcecao_SeSenhaAtualEstiverIncorreta()
        {
            var input = new AtualizarUsuarioInput 
            { 
                EmailAtual = "teste@teste.com", 
                EmailNovo = "emailteste@teste.com", 
                SenhaAtual = "123456", 
                SenhaNova = "1234657" 
            };

            var usuario = new Usuario { Id = 1, UsuarioSenha = "correta" };

            _mockUsuarioRepository.Setup(r => r.ObterPorEmail(input.EmailAtual)).ReturnsAsync(usuario);
            _mockCriptografiaService.Setup(c => c.Criptografar(input.SenhaAtual)).Returns("errada");

            await Assert.ThrowsAsync<Exception>(() => _usuarioService.Atualizar(input, 1));
        }

        [Fact]
        public async Task Atualizar_DeveAtualizarSenhaEEmail_QuandoDadosForemValidos()
        {
            var input = new AtualizarUsuarioInput
            {
                EmailAtual = "teste@teste.com",
                EmailNovo = "novo@teste.com",
                SenhaAtual = "senhaAtual",
                SenhaNova = "novaSenha"
            };

            var usuario = new Usuario
            {
                Id = 1,
                UsuarioSenha = "senhaAtualCriptografada"
            };

            _mockUsuarioRepository.Setup(r => r.ObterPorEmail(input.EmailAtual)).ReturnsAsync(usuario);
            _mockCriptografiaService.Setup(c => c.Criptografar(input.SenhaAtual)).Returns("senhaAtualCriptografada");
            _mockCriptografiaService.Setup(c => c.Criptografar(input.SenhaNova)).Returns("novaSenhaCriptografada");

            await _usuarioService.Atualizar(input, 1);

            Assert.Equal("novo@teste.com", usuario.UsuarioEmail);
            Assert.Equal("novaSenhaCriptografada", usuario.UsuarioSenha);
            _mockUsuarioRepository.Verify(r => r.Atualizar(usuario), Times.Once);
        }
    }
}
