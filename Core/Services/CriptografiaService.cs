using Core.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Core.Services
{
    public class CriptografiaService : ICriptografiaService
    {
        private readonly string _key;
        private readonly string _iv;

        public CriptografiaService(IConfiguration configuration)
        {
            _key = configuration["Criptografia:Key"];
            _iv = configuration["Criptografia:IV"];
        }

        public string Criptografar(string input)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.IV = Encoding.UTF8.GetBytes(_iv);

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(input);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public string Descriptografar(string input)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.IV = Encoding.UTF8.GetBytes(_iv);

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(Convert.FromBase64String(input));
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}
