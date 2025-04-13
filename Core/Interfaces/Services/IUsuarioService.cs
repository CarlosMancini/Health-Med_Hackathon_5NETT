using Core.Entities;

namespace Core.Interfaces.Service
{
    public interface IUsuarioService
    {
        Task<Usuario?> Autenticar(string email, string senha);
    }
}
