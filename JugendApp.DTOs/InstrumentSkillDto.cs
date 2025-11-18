namespace JugendApp.DTOs
{
    public class InstrumentSkillDto
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public InstrumentDto Instrument { get; set; } = new();

    }
}
