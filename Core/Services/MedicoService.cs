using Core.Entities;
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

        public async Task Cadastrar(Medico medico)
        {
            // TO DO: add validação de médico já cadastrado

            await _medicoRepository.Cadastrar(medico);
        }

        public async Task<Medico> ObterPorUsuarioId(int id)
        {
            return await _medicoRepository.ObterPorUsuarioId(id);
        }
    }
}
