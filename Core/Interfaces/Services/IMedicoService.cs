using Core.DTOs;
using Core.Entities;
using Core.Inputs.AdicionarUsuario;
using Core.Inputs.Atualizar;
using Core.Inputs.Pesquisar;

namespace Core.Interfaces.Services
{
    public interface IMedicoService : IServiceBase<Medico>
    {
        Task<Medico> ObterMedicoPorId(int usuarioId);
        Task<ICollection<Medico>> ObterPorEspecialidade(int especialidadeId);
        Task<ICollection<MedicoDisponivelDto>> PesquisarMedicosDisponiveis(FiltroPesquisaMedicoInput input);
        Task Cadastrar(CadastrarMedicoInput input);
        Task Atualizar(AtualizarMedicoInput input);
        Task Excluir(int usuarioId);
    }
}
