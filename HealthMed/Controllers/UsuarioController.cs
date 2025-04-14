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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ICriptografiaService _cpriptografiaService;

        public UsuarioController(IUsuarioService usuarioService, ICriptografiaService criptografiaService)
        {
            _usuarioService = usuarioService;
            _cpriptografiaService = criptografiaService;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(UsuarioInput usuarioInput)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                Usuario usuario = new Usuario
                {
                    UsuarioNome = usuarioInput.Nome,
                    UsuarioEmail = usuarioInput.Email,
                    UsuarioCPF = usuarioInput.UsuarioCPF,
                    UsuarioSenha = _cpriptografiaService.Criptografar(usuarioInput.Senha),
                    PerfilId = usuarioInput.PerfilId,
                    CriadoEm = DateTime.Now,
                };

                await _usuarioService.Cadastrar(usuario);

                return Ok("Usuário cadastrado com sucesso");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
