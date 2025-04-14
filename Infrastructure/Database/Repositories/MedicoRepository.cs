using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class MedicoRepository : RepositoryBase<Medico>, IMedicoRepository
    {
        public MedicoRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Medico> ObterPorUsuarioId(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.UsuarioId == id);
        }
    }
}
