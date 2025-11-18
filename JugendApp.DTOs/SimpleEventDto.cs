namespace JugendApp.DTOs
{
    public class SimpleEventDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PersonId { get; set; }
        public int LocationId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public LocationDto Location { get; set; } = new();

    }
}
