using Core.Entities;
using Core.Inputs.Atualizar;
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

        public async Task AtualizarStatus(AtualizarAgendamentoStatusInput input)
        {
            var agendamento = await _agendamentoRepository.ObterPorId(input.AgendamentoId);

            agendamento.AgendamentoStatusId = input.StatusId;

            if (input.StatusId == (int)AgendamentoStatusEnum.Cancelado)
                agendamento.MotivoCancelamentoId = input.MotivoCancelamentoId;
            else agendamento.MotivoCancelamentoId = null;

            await _agendamentoRepository.Atualizar(agendamento);
        }
    }
}
