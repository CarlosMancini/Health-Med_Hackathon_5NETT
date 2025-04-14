using Core.Entities;
using Core.Interfaces.Repository;

namespace Core.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Task<Usuario?> Autenticar(string email, string senha);
    }
}
