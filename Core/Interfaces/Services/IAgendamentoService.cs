using Core.Entities;
using Core.Inputs.Cadastrar;

namespace Core.Interfaces.Services
{
    public interface IAgendamentoService : IServiceBase<Agendamento>
    {
        Task AgendarConsulta(CadastrarAgendamentoInput input);
    }
}
