using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HealthMed.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int ObterUsuarioId(this ClaimsPrincipal user)
        {
            return int.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id) ? id : 0;
        }

        public static string ObterRole(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Role)?.Value ?? "Default";
        }

        public static IActionResult? ValidarPermissaoDeAcesso(this ClaimsPrincipal user, int idRecurso)
        {
            var usuarioId = user.ObterUsuarioId();
            var role = user.ObterRole();

            var ehAdmin = role == "Administrador";
            var ehMesmoUsuario = usuarioId == idRecurso;

            if (!ehAdmin && !ehMesmoUsuario)
                return new ForbidResult("Você não tem permissão para acessar esse recurso.");

            return null; // acesso permitido
        }
    }
}
