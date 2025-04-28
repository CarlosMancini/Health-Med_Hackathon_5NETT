using Core.Inputs.Autenticar;
using Core.Interfaces.Services;
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

        [HttpPost("medico")]
        public async Task<IActionResult> LoginMedico(AutenticacaoMedicoInput input)
        {
            try
            {
                input.Senha = _cpriptografiaService.Criptografar(input.Senha);
                var usuario = await _usuarioService.AutenticarMedico(input);
                if (usuario == null)
                    return Unauthorized("Usuário ou senha inválidos.");

                return Ok(new { Token = _tokenService.GerarToken(usuario) });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("paciente")]
        public async Task<IActionResult> LoginPaciente(AutenticacaoPacienteInput input)
        {
            try
            {
                input.Senha = _cpriptografiaService.Criptografar(input.Senha);
                var usuario = await _usuarioService.AutenticarPaciente(input);
                if (usuario == null)
                    return Unauthorized("Usuário ou senha inválidos.");

                return Ok(new { Token = _tokenService.GerarToken(usuario) });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
