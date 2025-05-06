using System.ComponentModel.DataAnnotations;

namespace Core.Inputs.Pesquisar
{
    public class FiltroPesquisaMedicoInput
    {
        [Required]
        public int EspecialidadeId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }
    }
}
