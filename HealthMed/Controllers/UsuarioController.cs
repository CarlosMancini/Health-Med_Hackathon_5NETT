using Core.Inputs.AdicionarUsuario;
using Core.Inputs.Atualizar;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("cadastrar-medico")]
        public async Task<IActionResult> CadastrarMedico(CadastrarMedicoInput input)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _usuarioService.CadastrarMedico(input);

                return Ok("Médico cadastrado com sucesso");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("cadastrar-paciente")]
        public async Task<IActionResult> CadastrarPaciente(CadastrarPacienteInput input)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _usuarioService.CadastrarPaciente(input);

                return Ok("Paciente cadastrado com sucesso");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("atualizar-medico")]
        public async Task<IActionResult> AtualizarMedico(AtualizarMedicoInput input)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _usuarioService.AtualizarMedico(input);

                return Ok("Cadastro de médico atualizado");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
