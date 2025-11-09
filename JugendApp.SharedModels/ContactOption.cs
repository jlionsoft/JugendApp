namespace JugendApp.SharedModels;

public class ContactOption
{
    public int Id { get; private set; }
    public ContactOptionType Type { get; private set; }
    public string Value { get; private set; }
    public bool IsValidated { get; private set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ContactOption"/> class for a new contact method not yet stored in the database.
    /// </summary>
    /// <param name="type">The <see cref="ContactOptionType"/> specifying the type of contact method (e.g., email, phone).</param>
    /// <param name="value">The contact detail as a string (e.g., email address or phone number).</param>
    /// <param name="isValidated">Indicates whether the contact method has been validated by the user via confirmation code.</param>
    public ContactOption(ContactOptionType type, string value, bool isValidated)
    {
        Type = type;
        Value = value;
        IsValidated = isValidated;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContactOption"/> class for an existing contact method stored in the database.
    /// </summary>
    /// <param name="id">Unique identifier of the <see cref="ContactOption"/> in the database.</param>
    /// <param name="type">The <see cref="ContactOptionType"/> specifying the type of contact method.</param>
    /// <param name="value">The contact detail as a string.</param>
    /// <param name="isValidated">Indicates whether the contact method has been validated by the user.</param>
    public ContactOption(int id, ContactOptionType type, string value, bool isValidated)
    {
        Id = id;
        Type = type;
        Value = value;
        IsValidated = isValidated;
    }

    
}

public enum ContactOptionType
{
    Email,
    Sms,
    WhatsApp
}