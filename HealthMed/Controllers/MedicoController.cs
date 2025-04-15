using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var medicos = await _medicoService.ObterTodos();
            return Ok(medicos);
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var medico = await _medicoService.ObterPorId(id);
            if (medico == null)
                return NotFound();

            return Ok(medico);
        }

        [HttpGet("{especialidadeId}")]
        public async Task<IActionResult> ObterPorEspecialidade(int especialidadeId)
        {
            try
            {
                var medicos = await _medicoService.ObterPorEspecialidade(especialidadeId);

                return Ok(medicos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
