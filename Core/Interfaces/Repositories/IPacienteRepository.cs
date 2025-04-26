using Core.Entities;
using Core.Interfaces.Repository;

namespace Core.Interfaces.Repositories
{
    public interface IPacienteRepository : IRepositoryBase<Paciente>
    {
        Task ExcluirPaciente(int id);
    }
}
