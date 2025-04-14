using Core.Entities;
using Core.Inputs;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorUsuarioId(int id)
        {
            var medico = await _medicoService.ObterPorUsuarioId(id);
            if (medico == null)
                return NotFound();

            return Ok(medico);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(MedicoInput medicoInput)
        {
            try
            {
                Medico medico = new Medico
                {
                    UsuarioId = medicoInput.UsuarioId,
                    CRM = medicoInput.CRM,
                };

                await _medicoService.Cadastrar(medico);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
