using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Usuario?> Autenticar(string email, string senha)
        {
            return await _context.Usuario
                .Include(u => u.Perfil)
                .FirstOrDefaultAsync(u => u.UsuarioEmail == email && u.UsuarioSenha == senha);
        }

        public async Task<Usuario?> ObterPorEmail(string email)
        {
            return await _context.Usuario
                .Include(u => u.Perfil)
                .FirstOrDefaultAsync(u => u.UsuarioEmail == email);
        }
    }
}
