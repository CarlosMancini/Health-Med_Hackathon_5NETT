using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IServiceBase<T> where T : EntityBase
    {
        Task<IList<T>> ObterTodos();
        Task<T> ObterPorId(int id);
    }
}
