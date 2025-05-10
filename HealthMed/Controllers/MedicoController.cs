using Core.Inputs.AdicionarUsuario;
using Core.Inputs.Atualizar;
using Core.Inputs.Pesquisar;
using Core.Interfaces.Services;
using HealthMed.Extensions;
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
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ObterTodos()
        {
            var medicos = await _medicoService.ObterTodos();
            return Ok(medicos);
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> ObterMedicoPorId(int usuarioId)
        {
            var medico = await _medicoService.ObterMedicoPorId(usuarioId);
            if (medico == null)
                return NotFound();

            return Ok(medico);
        }

        [HttpGet("{especialidadeId}")]
        public async Task<IActionResult> ObterPorEspecialidade(int especialidadeId)
        {
            var medicos = await _medicoService.ObterPorEspecialidade(especialidadeId);
            return Ok(medicos);
        }

        [HttpGet("pesquisar-medicos-disponiveis")]
        [Authorize(Roles = "Medico,Paciente")]
        public async Task<IActionResult> PesquisarMedicosDisponiveis([FromQuery] FiltroPesquisaMedicoDisponivelInput input)
        {
            var medicos = await _medicoService.PesquisarMedicosDisponiveis(input);
            return Ok(medicos);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Cadastrar(CadastrarMedicoInput input)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _medicoService.Cadastrar(input);

                return Ok("Médico cadastrado com sucesso");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Administrador,Medico")]
        public async Task<IActionResult> Atualizar(AtualizarMedicoInput input)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var resultadoValidacao = User.ValidarPermissaoDeAcesso(input.Id);
                if (resultadoValidacao is ForbidResult) return resultadoValidacao;

                await _medicoService.Atualizar(input);

                return Ok("Cadastro de médico atualizado");
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
                await _medicoService.Excluir(usuarioId);

                return Ok("Cadastro de médico excluído com sucesso");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
