using Core.Entities;
using Core.Inputs.Autenticar;
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

        public async Task<Usuario?> AutenticarMedico(AutenticacaoMedicoInput input)
        {
            var usuario = await _context.Medico
                .Include(m => m.Usuario)
                .Include(m => m.Usuario.Perfil)
                .FirstOrDefaultAsync(m =>
                    m.MedicoCRM.Trim().ToLower() == input.CRM.Trim().ToLower() &&
                    m.Usuario.UsuarioSenha == input.Senha);

            return usuario?.Usuario;
        }

        public async Task<Usuario?> AutenticarPaciente(AutenticacaoPacienteInput input)
        {
            var usuario = await _context.Usuario
                .Include(u => u.Perfil)
                .FirstOrDefaultAsync(u =>
                    (u.UsuarioEmail.Trim().ToLower() == input.Login.Trim().ToLower() ||
                     u.UsuarioCPF.Trim().Replace(".", "").Replace("-", "") == input.Login.Trim().Replace(".", "").Replace("-", ""))
                    && u.UsuarioSenha == input.Senha);

            return usuario;
        }

        public async Task<Usuario?> ObterPorEmail(string email)
        {
            return await _context.Usuario
                .Include(u => u.Perfil)
                .FirstOrDefaultAsync(u => u.UsuarioEmail == email);
        }

        public async Task<Usuario> CadastrarUsuario(Usuario usuario)
        {
            await _context.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }
    }
}
