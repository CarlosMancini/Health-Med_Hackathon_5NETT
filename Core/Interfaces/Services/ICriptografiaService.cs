namespace Core.Interfaces.Services
{
    public interface ICriptografiaService
    {
        public string Criptografar(string input);
        public string Descriptografar(string input);
    }
}
