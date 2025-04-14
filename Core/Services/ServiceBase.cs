using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;

namespace Core.Services
{
    public class ServiceBase<T> : IServiceBase<T> where T : EntityBase
    {
        private readonly IRepositoryBase<T> _repository;

        public ServiceBase(IRepositoryBase<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> ObterPorId(int id)
        {
            return await _repository.ObterPorId(id);
        }

        public async Task<IList<T>> ObterTodos()
        {
            return await _repository.ObterTodos();
        }
    }
}
