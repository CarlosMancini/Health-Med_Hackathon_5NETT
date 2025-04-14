using System.ComponentModel.DataAnnotations;

namespace Core.Inputs
{
    public class MedicoInput
    {
        [Required(ErrorMessage = "É obrigatório informar o usuário cadastrado.")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o CRM.")]
        public string CRM { get; set; }
    }
}
