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

        //[HttpPut]
        //public async Task<IActionResult> Atualizar(AtualizarUsuarioInput input)
        //{

        //}
    }
}
