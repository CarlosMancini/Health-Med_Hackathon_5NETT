using Core.Entities;
using Core.Inputs.AdicionarUsuario;
using Core.Inputs.Atualizar;
using Core.Inputs.Autenticar;
using Core.Utils.Enums;

namespace Core.Interfaces.Services
{
    public interface IUsuarioService : IServiceBase<Usuario>
    {
        Task<Usuario?> ObterPorEmail(string email);
        Task<Usuario?> AutenticarMedico(AutenticacaoMedicoInput input);
        Task<Usuario?> AutenticarPaciente(AutenticacaoPacienteInput input);
        Task<Usuario> CadastrarUsuario(CadastrarUsuarioInput input, PerfilEnum perfil);
        Task Atualizar(AtualizarUsuarioInput input, int usuarioLogadoId);
    }
}