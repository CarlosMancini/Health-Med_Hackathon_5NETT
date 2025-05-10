using Core.Inputs.Atualizar;
using Core.Interfaces.Services;
using HealthMed.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpPut]
        public async Task<IActionResult> Atualizar(AtualizarUsuarioInput input)
        {
            try
            {
                var usuarioLogadoId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

                var resultadoValidacao = User.ValidarPermissaoDeAcesso(usuarioLogadoId);
                if (resultadoValidacao is ForbidResult) return resultadoValidacao;

                await _usuarioService.Atualizar(input, usuarioLogadoId);

                return Ok("Credenciais atualizadas com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
