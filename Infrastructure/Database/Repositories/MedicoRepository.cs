using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class MedicoRepository : RepositoryBase<Medico>, IMedicoRepository
    {
        public MedicoRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Medico?> ObterPorEmail(string email)
        {
            return await _context.Medico
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(u => u.Usuario.UsuarioEmail == email);
        }
    }
}
