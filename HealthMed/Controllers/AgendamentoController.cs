using Core.Inputs.Cadastrar;
using Core.Interfaces.Services;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoService _agendamentoService;

        public AgendamentoController(IAgendamentoService agendamentoService)
        {
            _agendamentoService = agendamentoService;
        }

        // TO DO: não permitir agendamento duplicado

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var agendamentos = await _agendamentoService.ObterTodos();
            return Ok(agendamentos);
        }

        //[HttpGet("obter-por-medico")]
        //public async Task<IActionResult> ObterPorMedico(int medicoId)
        //{

        //}

        [HttpPost]
        public async Task<IActionResult> AgendarConsulta(CadastrarAgendamentoInput input)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _agendamentoService.AgendarConsulta(input);

                return Ok("Agendamento realizado com sucesso");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
