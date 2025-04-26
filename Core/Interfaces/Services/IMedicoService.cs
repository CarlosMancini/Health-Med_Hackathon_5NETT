using Core.Entities;
using Core.Inputs.AdicionarUsuario;
using Core.Inputs.Atualizar;

namespace Core.Interfaces.Services
{
    public interface IMedicoService : IServiceBase<Medico>
    {
        Task<Medico> ObterMedicoPorId(int usuarioId);
        Task<ICollection<Medico>> ObterPorEspecialidade(int especialidadeId);
        Task Cadastrar(CadastrarMedicoInput input);
        Task Atualizar(AtualizarMedicoInput input);
        Task Excluir(int usuarioId);
    }
}
