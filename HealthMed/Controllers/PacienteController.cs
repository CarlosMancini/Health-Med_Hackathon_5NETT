using Core.Inputs.AdicionarUsuario;
using Core.Inputs.Atualizar;
using Core.Interfaces.Services;
using HealthMed.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Medico")]
        public async Task<IActionResult> ObterTodos()
        {
            var pacientes = await _pacienteService.ObterTodos();
            return Ok(pacientes);
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> ObterPacientePorId(int usuarioId)
        {
            var paciente = await _pacienteService.ObterPorId(usuarioId);
            if (paciente == null)
                return NotFound();

            return Ok(paciente);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Cadastrar(CadastrarPacienteInput input)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _pacienteService.Cadastrar(input);

                return Ok("Paciente cadastrado com sucesso");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Administrador,Paciente")]
        public async Task<IActionResult> Atualizar(AtualizarPacienteInput input)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var resultadoValidacao = User.ValidarPermissaoDeAcesso(input.Id);
                if (resultadoValidacao is ForbidResult) return resultadoValidacao;

                await _pacienteService.Atualizar(input);

                return Ok("Cadastro de paciente atualizado");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Excluir(int usuarioId)
        {
            try
            {
                await _pacienteService.Excluir(usuarioId);

                return Ok("Cadastro de paciente cadastrado com sucesso");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
