using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Medico
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string CRM { get; set; }

        public Usuario Usuario { get; set; }
        public ICollection<MedicoEspecialidade> Especialidades { get; set; }
    }
}
