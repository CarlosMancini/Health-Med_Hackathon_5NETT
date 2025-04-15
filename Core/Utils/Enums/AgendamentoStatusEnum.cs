using System.ComponentModel;

namespace Core.Utils.Enums
{
    public enum AgendamentoStatusEnum
    {
        Pendente = 1,
        Agendado,
        [Description("Concluído")]
        Concluido,
        Cancelado
    }
}
