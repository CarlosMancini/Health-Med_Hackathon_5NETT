using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Core.Inputs.AdicionarUsuario
{
    public class CadastrarMedicoInput : CadastrarUsuarioInput
    {
        [Required(ErrorMessage = "É obrigatório informar o CRM.")]
        public required string CRM { get; set; }

        [Required(ErrorMessage = "É obrigatório informar pelo menos uma especialidade")]
        public required ICollection<int> EspecialidadesId { get; set; }
    }
}
