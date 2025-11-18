namespace JugendApp.DTOs
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public AddressDto Address { get; set; } = new();

    }
}
