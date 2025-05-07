using Core.Entities;
using Core.Inputs.Cadastrar;
using Core.Inputs.Pesquisar;
using Core.Interfaces.Repository;

namespace Core.Interfaces.Repositories
{
    public interface IAgendamentoRepository : IRepositoryBase<Agendamento>
    {
        Task<ICollection<Agendamento>> Pesquisar(FiltroPesquisaAgendamentoInput input);
        Task AgendarConsulta(CadastrarAgendamentoInput input);
    }
}
