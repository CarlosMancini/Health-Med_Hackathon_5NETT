using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Database.Repository;

namespace Infrastructure.Database.Repositories
{
    public class AgendamentoRepository : RepositoryBase<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(ApplicationDbContext context) : base(context) { }
    }
}
