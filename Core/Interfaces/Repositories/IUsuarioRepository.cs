using Core.Entities;
using Core.Inputs.Autenticar;
using Core.Interfaces.Repository;

namespace Core.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Task<Usuario?> AutenticarMedico(AutenticacaoMedicoInput input);
        Task<Usuario?> AutenticarPaciente(AutenticacaoPacienteInput input);
        Task<Usuario?> ObterPorEmail(string email);
        Task<Usuario> CadastrarUsuario(Usuario usuario);
    }
}
