namespace JugendApp.DTOs
{
    public class ContactOptionDto
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Value { get; set; } = string.Empty;
        public bool IsValidated { get; set; }

    }
}
