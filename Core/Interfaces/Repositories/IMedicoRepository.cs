using Core.Entities;
using Core.Interfaces.Repository;

namespace Core.Interfaces.Repositories
{
    public interface IMedicoRepository : IRepositoryBase<Medico>
    {
        Task<ICollection<Medico>> ObterPorEspecialidade(int especialidadeId);
    }
}
