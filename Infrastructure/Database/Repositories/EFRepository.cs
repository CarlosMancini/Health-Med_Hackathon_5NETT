using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public EFRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public Task Atualizar(T entidade)
        {
            throw new NotImplementedException();
        }

        public Task Cadastrar(T entidade)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> ObterTodos()
        {
            throw new NotImplementedException();
        }
    }
}
