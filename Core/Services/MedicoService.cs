using Core.Entities;
using Core.Inputs.AdicionarUsuario;
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

        public async Task<Medico> ObterMedicoPorId(int id)
        {
            return await _medicoRepository.ObterMedicoPorId(id);
        }

        public async Task<ICollection<Medico>> ObterPorEspecialidade(int especialidadeId)
        {
            return await _medicoRepository.ObterPorEspecialidade(especialidadeId);
        }

        public async Task Cadastrar(int usuarioId, CadastrarMedicoInput input)
        {
            Medico medico = new Medico
            {
                Id = usuarioId,
                MedicoCRM = input.CRM,

                MedicoEspecialidades = MapearMedicoEspecialidades(usuarioId, input.EspecialidadesId)
            };

            await _medicoRepository.Cadastrar(medico);
        }

        public async Task Atualizar(AtualizarMedicoInput input)
        {
            var medico = await _medicoRepository.ObterMedicoPorId(input.Id);
            if (medico == null)
                throw new Exception("Médico não encontrado");

            medico.MedicoCRM = input.CRM;
            medico.MedicoEspecialidades.Clear();

            medico.MedicoEspecialidades = MapearMedicoEspecialidades(input.Id, input.EspecialidadesId);

            await _medicoRepository.Atualizar(medico);
        }

        public List<MedicoEspecialidade> MapearMedicoEspecialidades(int id, ICollection<int> especialidadesId)
        {
            List <MedicoEspecialidade> medicoEspecialidades = new List<MedicoEspecialidade>();

            foreach (var especialidadeId in especialidadesId)
            {
                medicoEspecialidades.Add(new MedicoEspecialidade
                {
                    MedicoId = id,
                    EspecialidadeId = especialidadeId
                });
            }

            return medicoEspecialidades;
        }
    }
}
