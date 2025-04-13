using Core.Interfaces.Service;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AutenticacaoController : ControllerBase
{
    private readonly IUsuarioService _autenticacaoService;
    private readonly ICriptografiaService _cpriptografiaService;
    private readonly ITokenService _tokenService;
        
    public AutenticacaoController(IUsuarioService autenticacaoService, ICriptografiaService criptografiaService, ITokenService tokenService)
    {
        _autenticacaoService = autenticacaoService;
        _cpriptografiaService = criptografiaService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var senha = _cpriptografiaService.Criptografar(request.Password);
        var usuario = await _autenticacaoService.Autenticar(request.Email, senha);
        if (usuario == null)
            return Unauthorized("Usuário ou senha inválidos.");

        var token = _tokenService.GerarToken(usuario);

        return Ok(new { Token = token });
    }
}
