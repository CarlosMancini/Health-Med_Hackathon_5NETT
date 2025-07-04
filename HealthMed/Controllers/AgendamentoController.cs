﻿using Core.Inputs.Atualizar;
using Core.Inputs.Cadastrar;
using Core.Inputs.Pesquisar;
using Core.Interfaces.Services;
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

        [HttpGet("pesquisar")]
        [Authorize(Roles = "Medico,Paciente")]
        public async Task<IActionResult> Pesquisar([FromQuery] FiltroPesquisaAgendamentoInput input)
        {
            var agendamentos = await _agendamentoService.Pesquisar(input);
            return Ok(agendamentos);
        }

        [HttpPost]
        [Authorize(Roles = "Paciente")]
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

        [HttpPut("atualizar-status")]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> AtualizarStatus(AtualizarAgendamentoStatusInput input)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _agendamentoService.AtualizarStatus(input);

                return Ok("Status do agendamento atualizado com sucesso");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
