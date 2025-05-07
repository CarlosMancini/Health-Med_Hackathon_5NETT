using System.ComponentModel.DataAnnotations;

namespace Core.Inputs.Pesquisar
{
    public class FiltroPesquisaMedicoDisponivelInput
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
