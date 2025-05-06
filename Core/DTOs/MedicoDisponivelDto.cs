namespace Core.DTOs
{
    public class MedicoDisponivelDto
    {
        public int MedicoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CRM { get; set; } = string.Empty;
        public List<string> Especialidades { get; set; } = new();
        public List<DiaDisponivelDto> DiasDisponiveis { get; set; }
    }
}
