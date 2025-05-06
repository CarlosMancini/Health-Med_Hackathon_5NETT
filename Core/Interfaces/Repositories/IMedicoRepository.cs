using Core.DTOs;
using Core.Entities;
using Core.Inputs.Pesquisar;
using Core.Interfaces.Repository;

namespace Core.Interfaces.Repositories
{
    public interface IMedicoRepository : IRepositoryBase<Medico>
    {
        Task<Medico> ObterMedicoPorId(int id);
        Task<ICollection<Medico>> ObterPorEspecialidade(int especialidadeId);
        Task<ICollection<MedicoDisponivelDto>> PesquisarMedicosDisponiveis(FiltroPesquisaMedicoInput input);
        Task ExcluirMedico(int id);
    }
}
