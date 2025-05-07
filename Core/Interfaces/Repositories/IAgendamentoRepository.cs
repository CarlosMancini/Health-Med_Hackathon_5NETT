using Core.Entities;
using Core.Inputs.Cadastrar;
using Core.Interfaces.Repository;

namespace Core.Interfaces.Repositories
{
    public interface IAgendamentoRepository : IRepositoryBase<Agendamento>
    {
        Task AgendarConsulta(CadastrarAgendamentoInput input);
    }
}
