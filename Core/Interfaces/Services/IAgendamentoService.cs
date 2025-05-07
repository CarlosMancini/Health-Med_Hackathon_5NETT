using Core.Entities;
using Core.Inputs.Atualizar;
using Core.Inputs.Cadastrar;
using Core.Inputs.Pesquisar;

namespace Core.Interfaces.Services
{
    public interface IAgendamentoService : IServiceBase<Agendamento>
    {
        Task<ICollection<Agendamento>> Pesquisar(FiltroPesquisaAgendamentoInput input);
        Task AgendarConsulta(CadastrarAgendamentoInput input);
        Task AtualizarStatus(AtualizarAgendamentoStatusInput input);
    }
}
