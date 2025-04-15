using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Database.Repository;

namespace Infrastructure.Database.Repositories
{
    public class MedicoRepository : RepositoryBase<Medico>, IMedicoRepository
    {
        public MedicoRepository(ApplicationDbContext context) : base(context) { }
    }
}
