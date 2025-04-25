using Core.Entities;
using Core.Inputs.AdicionarUsuario;
using Core.Inputs.Atualizar;

namespace Core.Interfaces.Services
{
    public interface IPacienteService : IServiceBase<Paciente>
    {
        Task Cadastrar(CadastrarPacienteInput cadastrar);
        Task Atualizar(AtualizarPacienteInput input);
    }
}
