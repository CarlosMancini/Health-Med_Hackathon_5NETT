using Core.DTOs;
using Core.Entities;
using Core.Inputs.AdicionarUsuario;
using Core.Inputs.Atualizar;
using Core.Inputs.Compartilhados;
using Core.Inputs.Pesquisar;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Utils.Enums;

namespace Core.Services
{
    public class MedicoService : ServiceBase<Medico>, IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IUsuarioService _usuarioService;

        public MedicoService(IMedicoRepository medicoRepository, IUsuarioService usuarioService) : base(medicoRepository)
        {
            _medicoRepository = medicoRepository;
            _usuarioService = usuarioService;
        }

        public async Task<Medico> ObterMedicoPorId(int id)
        {
            return await _medicoRepository.ObterMedicoPorId(id);
        }

        public async Task<ICollection<Medico>> ObterPorEspecialidade(int especialidadeId)
        {
            return await _medicoRepository.ObterPorEspecialidade(especialidadeId);
        }

        public async Task<ICollection<MedicoDisponivelDto>> PesquisarMedicosDisponiveis(FiltroPesquisaMedicoInput input)
        {
            return await _medicoRepository.PesquisarMedicosDisponiveis(input);
        }

        public async Task Cadastrar(CadastrarMedicoInput input)
        {
            Usuario usuario = await _usuarioService.CadastrarUsuario(input, PerfilEnum.Medico);

            Medico medico = new Medico
            {
                Id = usuario.Id,
                MedicoCRM = input.CRM,

                MedicoEspecialidades = MapearMedicoEspecialidades(usuario.Id, input.EspecialidadesId)
            };

            await _medicoRepository.Cadastrar(medico);
        }

        public async Task Atualizar(AtualizarMedicoInput input)
        {
            var medico = await _medicoRepository.ObterMedicoPorId(input.Id) 
                ?? throw new Exception("Médico não encontrado");

            medico.MedicoCRM = input.CRM;
            medico.MedicoEspecialidades.Clear();
            medico.MedicoEspecialidades = MapearMedicoEspecialidades(input.Id, input.EspecialidadesId);

            medico.HorariosDisponiveis.Clear();
            medico.HorariosDisponiveis = MapearHorariosDisponiveis(input.Id, input.HorariosDisponiveis);

            await _medicoRepository.Atualizar(medico);
        }

        public static List<MedicoEspecialidade> MapearMedicoEspecialidades(int id, ICollection<int> especialidadesId)
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

        private static List<HorarioDisponivel> MapearHorariosDisponiveis(int medicoId, ICollection<HorarioDisponivelInput> horariosDisponiveisInputs)
        {
            return horariosDisponiveisInputs.Select(h => new HorarioDisponivel
            {
                MedicoId = medicoId,
                HorarioDisponivelDiaSemana = h.DiaSemana,
                HorarioDisponivelHoraInicio = h.HoraInicio,
                HorarioDisponivelHoraFim = h.HoraFim
            }).ToList();
        }

        public async Task Excluir(int usuarioId)
        {
            await _medicoRepository.ExcluirMedico(usuarioId);
        }
    }
}
