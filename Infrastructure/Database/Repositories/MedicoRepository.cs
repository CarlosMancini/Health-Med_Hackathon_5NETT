using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class MedicoRepository : RepositoryBase<Medico>, IMedicoRepository
    {
        public MedicoRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Medico> ObterMedicoPorId(int id)
        {
            return await _context.Medico
                .Include(m => m.MedicoEspecialidades)
                .Include(m => m.HorariosDisponiveis)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<ICollection<Medico>> ObterPorEspecialidade(int especialidadeId)
        {
            return await _context.Medico
                .Where(m => m.MedicoEspecialidades.Any(me => me.EspecialidadeId == especialidadeId))
                .Include(m => m.MedicoEspecialidades)
                .ThenInclude(me => me.Especialidade)
                .ToListAsync();
        }
    }
}
