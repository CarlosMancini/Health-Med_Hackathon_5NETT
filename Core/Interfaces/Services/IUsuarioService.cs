using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IUsuarioService : IServiceBase<Usuario>
    {
        Task<Usuario?> Autenticar(string email, string senha);
        Task Cadastrar(Usuario usuarioInput);
    }
}
