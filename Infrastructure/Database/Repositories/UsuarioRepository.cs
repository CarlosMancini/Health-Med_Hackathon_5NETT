using Core.Entities;
using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> Autenticar(string email, string senha)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
        }
    }
}
