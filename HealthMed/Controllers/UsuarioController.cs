using Core.Inputs.Atualizar;
using Core.Interfaces.Services;
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
                // Obtém o ID do usuário logado a partir do token
                var usuarioLogadoId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

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
