using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Core.Services
{
    public class AgendamentoService : ServiceBase<Agendamento>, IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;

        public AgendamentoService(IAgendamentoRepository agendamentoRepository) : base(agendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
        }
    }
}
