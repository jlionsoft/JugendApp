namespace JugendApp.SharedModels.Person;

public class Instrument
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="Instrument"/> class with a database ID and a name.
    /// </summary>
    /// <param name="id">Unique identifier of the <see cref="Instrument"/> in the database.</param>
    /// <param name="name">Name of the <see cref="Instrument"/>.</param>
    public Instrument(int id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Instrument"/> class for a new instrument not yet stored in the database.
    /// </summary>
    /// <param name="name">Name of the new <see cref="Instrument"/>.</param>
    public Instrument(string name)
    {
        Name = name;
    }


    override public string ToString() => Name;
}