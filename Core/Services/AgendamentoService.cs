using Core.Entities;
using Core.Inputs.Cadastrar;
using Core.Inputs.Pesquisar;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Utils.Enums;

namespace Core.Services
{
    public class AgendamentoService : ServiceBase<Agendamento>, IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;

        public AgendamentoService(IAgendamentoRepository agendamentoRepository) : base(agendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
        }

        public async Task<ICollection<Agendamento>> Pesquisar(FiltroPesquisaAgendamentoInput input)
        {
            return await _agendamentoRepository.Pesquisar(input);
        }

        public async Task AgendarConsulta(CadastrarAgendamentoInput input)
        {
            await _agendamentoRepository.AgendarConsulta(input);
        }
    }
}
