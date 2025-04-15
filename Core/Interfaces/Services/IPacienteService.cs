using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IPacienteService : IServiceBase<Paciente>
    {
        Task Cadastrar(Paciente paciente);
    }
}
