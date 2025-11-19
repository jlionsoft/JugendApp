using JugendApp.SharedModels.Events;
using JugendApp.SharedModels.Groups;

namespace JugendApp.SharedModels.Person;

public class Person
{
    public int Id { get; set; }
    public string Firstname { get; private set; } = string.Empty;
    public string Lastname { get; private set; } = string.Empty;

    public ICollection<ContactOption> ContactOptions { get; private set; } = [];

    public void AddContact(ContactOption contact)
    {
        if (contact == null) throw new ArgumentNullException(nameof(contact));
        contact.Person = this;
        ContactOptions.Add(contact);
    }
    public void RemoveContact(ContactOption contact)
    {
        if (contact == null) throw new ArgumentNullException(nameof(contact));
        ContactOptions.Remove(contact);
        contact.Person = null;                  // optional, löst Beziehung im Objektgraph
    }

    public ICollection<InstrumentSkill> Instruments { get; private set; } = [];

    public void AddOrUpdateInstrument(Instrument instrument, int level)
    {
        if (instrument == null) throw new ArgumentNullException(nameof(instrument));
        var existing = Instruments.SingleOrDefault(s => s.InstrumentId == instrument.Id);
        if (existing != null)
        {
            existing.SetLevel(level);
            return;
        }
        var skill = new InstrumentSkill(instrument.Id, level);
        skill.AttachToPerson(this);
        skill.SetInstrument(instrument);
        Instruments.Add(skill);
    }
    public void RemoveInstrument(int instrumentId)
    {
        var existing = Instruments.SingleOrDefault(s => s.InstrumentId == instrumentId);
        if (existing == null) return;
        Instruments.Remove(existing);
        existing.Detach();
    }

    public ICollection<Invitation> Invitations { get; set; } = [];
    public Person() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Person"/> class with a specified ID and details.
    /// </summary>
    /// <param name="id">The unique identifier for the person.</param>
    /// <param name="firstname">The person's first name.</param>
    /// <param name="lastname">The person's last name.</param>
    /// <param name="contactOptions">A collection of contact details for the person.</param>
    /// <param name="instruments">A collection of instruments the person plays, including skill levels.</param>
    public Person(int id, string firstname, string lastname,  ICollection<ContactOption> contactOptions, ICollection<InstrumentSkill> instruments)
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
    public Person(string  firstname, string lastname, ICollection<ContactOption> contactOptions, ICollection<InstrumentSkill> instruments)
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