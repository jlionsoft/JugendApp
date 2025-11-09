namespace JugendApp.SharedModels;

public class Person
{
    public int Id { get; set; }
    public string Firstname { get; private set; } = string.Empty;
    public string Lastname { get; private set; } = string.Empty;

    public IEnumerable<ContactOption> ContactOptions { get; private set; } = [];
    
    public IEnumerable<InstrumentSkill> Instruments { get; private set; } = [];
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Person"/> class with a specified ID and details.
    /// </summary>
    /// <param name="id">The unique identifier for the person.</param>
    /// <param name="firstname">The person's first name.</param>
    /// <param name="lastname">The person's last name.</param>
    /// <param name="contactOptions">A collection of contact details for the person.</param>
    /// <param name="instruments">A collection of instruments the person plays, including skill levels.</param>
    public Person(int id, string firstname, string lastname,  IEnumerable<ContactOption> contactOptions, IEnumerable<InstrumentSkill> instruments)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        ContactOptions = contactOptions;
        Instruments = instruments;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="Person"/> class without an explicit ID.
    /// </summary>
    /// <param name="firstname">The person's first name.</param>
    /// <param name="lastname">The person's last name.</param>
    /// <param name="contactOptions">A collection of contact details for the person.</param>
    /// <param name="instruments">A collection of instruments the person plays, including skill levels.</param>
    public Person(string  firstname, string lastname, IEnumerable<ContactOption> contactOptions, IEnumerable<InstrumentSkill> instruments)
    {
        Firstname = firstname;
        Lastname = lastname;
        ContactOptions = contactOptions;
        Instruments = instruments;
    }
    
    /// <summary>
    /// Checks if the person plays an instrument with the specified name.
    /// </summary>
    /// <param name="name">The name of the instrument to check for (e.g., "Guitar", "Piano").</param>
    /// <returns>True if an instrument with that name is found in the list; otherwise, false.</returns>
    bool PlaysInstrument(string name)
    {
        return Instruments.Any(i => i.Instrument.Name == name);
    }

    /// <summary>
    /// Checks if the person plays the specified instrument object.
    /// </summary>
    /// <param name="instrument">The specific Instrument object reference to check for.</param>
    /// <returns>True if the exact instrument object is found in the list; otherwise, false.</returns>
    bool PlaysInstrument(Instrument instrument)
    {
        return Instruments.Any(i => i.Instrument == instrument);
    }
    
    override public string ToString() => $"{Firstname} - {Lastname}";
}