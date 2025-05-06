namespace Core.DTOs
{
    public class DiaDisponivelDto
    {
        public DateTime Data { get; set; }
        public string DiaSemana { get; set; }
        public List<HorarioDisponivelDto> Horarios { get; set; }
    }
}
