using Core.Validations.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Core.Inputs.Autenticar
{
    public class AutenticacaoPacienteInput
    {
        [Required(ErrorMessage = "O campo Login (CPF ou Email) é obrigatório.")]
        [CpfOuEmail]
        public required string Login { get; set; } 

        [Required(ErrorMessage = "Senha não informada.")]
        public required string Senha { get; set; }
    }
}
