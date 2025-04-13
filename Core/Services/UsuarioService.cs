using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Interfaces.Services;

namespace Core.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _autenticacaoRepository;
        private readonly ICriptografiaService _criptografiaService;

        public UsuarioService(IUsuarioRepository autenticacaoRepository, ICriptografiaService criptografiaService)
        {
            _autenticacaoRepository = autenticacaoRepository;
            _criptografiaService = criptografiaService;
        }

        public async Task<Usuario?> Autenticar(string email, string senha)
        {
            var usuario = await _autenticacaoRepository.Autenticar(email, senha);

            if (usuario == null || usuario.Senha != senha) // aqui você pode aplicar hashing e comparar
                return null;

            return usuario;
        }
    }
}
