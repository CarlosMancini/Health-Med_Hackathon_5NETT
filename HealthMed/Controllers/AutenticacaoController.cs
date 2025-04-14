using Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ICriptografiaService _cpriptografiaService;
        private readonly ITokenService _tokenService;

        public AutenticacaoController(IUsuarioService usuarioService, ICriptografiaService criptografiaService, ITokenService tokenService)
        {
            _usuarioService = usuarioService;
            _cpriptografiaService = criptografiaService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var senha = _cpriptografiaService.Criptografar(request.Password);
            var usuario = await _usuarioService.Autenticar(request.Email, senha);
            if (usuario == null)
                return Unauthorized("Usuário ou senha inválidos.");

            var token = _tokenService.GerarToken(usuario);

            return Ok(new { Token = token });
        }
    }
}
