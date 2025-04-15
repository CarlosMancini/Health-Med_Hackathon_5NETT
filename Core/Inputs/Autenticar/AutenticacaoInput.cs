using System.ComponentModel.DataAnnotations;

namespace Core.Inputs.Autenticar
{
    public class AutenticacaoInput
    {
        [Required(ErrorMessage = "E-mail não informado.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha não informada.")]
        public string Senha { get; set; }
    }
}
