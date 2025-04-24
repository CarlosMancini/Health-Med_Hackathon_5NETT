using Core.Entities;
using Core.Inputs.AdicionarUsuario;
using Core.Inputs.Atualizar;

namespace Core.Interfaces.Services
{
    public interface IUsuarioService : IServiceBase<Usuario>
    {
        Task<Usuario?> Autenticar(string email, string senha);
        Task CadastrarMedico(CadastrarMedicoInput input);
        Task CadastrarPaciente(CadastrarPacienteInput input);
    }
}
