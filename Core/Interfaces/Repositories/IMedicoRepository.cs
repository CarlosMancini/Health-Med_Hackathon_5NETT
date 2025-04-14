using Core.Entities;
using Core.Interfaces.Repository;

namespace Core.Interfaces.Repositories
{
    public interface IMedicoRepository : IRepositoryBase<Medico>
    {
        Task<Medico> ObterPorUsuarioId(int id);
    }
}
