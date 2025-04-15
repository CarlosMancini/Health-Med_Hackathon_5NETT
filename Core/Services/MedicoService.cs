using Core.Entities;
using Core.Inputs.Atualizar;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Core.Services
{
    public class MedicoService : ServiceBase<Medico>, IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;

        public MedicoService(IMedicoRepository medicoRepository) : base(medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<ICollection<Medico>> ObterPorEspecialidade(int especialidadeId)
        {
            return await _medicoRepository.ObterPorEspecialidade(especialidadeId);
        }
    }
}
