using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class PacienteRepository : RepositoryBase<Paciente>, IPacienteRepository
    {
        private readonly ApplicationDbContext _context;

        public PacienteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<Paciente?> ObterPorEmail(string email)
        {
            return await _context.Paciente
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(u => u.Usuario.UsuarioEmail == email);
        }
    }
}
