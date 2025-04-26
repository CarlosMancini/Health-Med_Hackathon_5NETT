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

        public async Task ExcluirPaciente(int id)
        {
            var paciente = await _context.Paciente
                .FirstOrDefaultAsync(m => m.Id == id);

            if (paciente == null)
                throw new Exception("Paciente não encontrado");

            _context.Paciente.Remove(paciente);

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario != null)
                _context.Usuario.Remove(usuario);

            await _context.SaveChangesAsync();
        }
    }
}
