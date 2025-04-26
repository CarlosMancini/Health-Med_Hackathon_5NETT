using Core.Entities;
using Core.Inputs.AdicionarUsuario;
using Core.Inputs.Atualizar;
using Core.Utils.Enums;

namespace Core.Interfaces.Services
{
    public interface IUsuarioService : IServiceBase<Usuario>
    {
        Task<Usuario?> Autenticar(string email, string senha);
        Task<Usuario> CadastrarUsuario(CadastrarUsuarioInput input, PerfilEnum perfil);
        Task Atualizar(AtualizarUsuarioInput input);
    }
}