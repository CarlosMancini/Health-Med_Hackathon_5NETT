using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> Autenticar(string email, string senha);
    }
}
