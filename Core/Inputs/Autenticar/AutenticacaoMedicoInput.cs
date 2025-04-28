using System.ComponentModel.DataAnnotations;

namespace Core.Inputs.Autenticar
{
    public class AutenticacaoMedicoInput
    {
        [Required(ErrorMessage = "CRM não informado.")]
        public required string CRM { get; set; }

        [Required(ErrorMessage = "Senha não informada.")]
        public required string Senha { get; set; }
    }
}
