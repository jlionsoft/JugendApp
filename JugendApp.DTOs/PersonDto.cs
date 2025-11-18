namespace JugendApp.DTOs
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public List<ContactOptionDto> ContactOptions { get; set; } = new();
        public List<InstrumentSkillDto> Instruments { get; set; } = new();

    }
}
