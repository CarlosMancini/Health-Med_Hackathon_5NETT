using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface ITokenService
    {
        public string GerarToken(Usuario usuario);
    }
}
