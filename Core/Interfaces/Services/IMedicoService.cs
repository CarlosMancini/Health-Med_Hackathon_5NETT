using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IMedicoService : IServiceBase<Medico>
    {
        Task<Medico> ObterPorUsuarioId(int id);
        Task Cadastrar(Medico medico);
    }
}
