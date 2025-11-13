namespace JugendApp.SharedModels.Person;

public class InstrumentSkill
{
    public int Id { get; private set; }
    public int InstrumentId { get; set; }
    public Instrument Instrument { get; private set; }
    public int Level { get; private set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="InstrumentSkill"/> class with a database ID, 
    /// an <see cref="Instrument"/> object, and a skill level.
    /// </summary>
    /// <param name="id">Unique identifier from the database.</param>
    /// <param name="instrument">The <see cref="Instrument"/> object associated with the skill.</param>
    /// <param name="level">Skill level ranging from 1 (beginner) to 10 (expert).</param>
    public InstrumentSkill(int id, Instrument instrument, int level)
    {
        Id = id;
        Instrument = instrument;
        Level = level;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InstrumentSkill"/> class using an instrument ID and skill level.
    /// </summary>
    /// <param name="instrumentId">Identifier of the <see cref="Instrument"/>.</param>
    /// <param name="level">Skill level ranging from 1 (beginner) to 10 (expert).</param>
    public InstrumentSkill(int instrumentId, int level)
    {
        InstrumentId = instrumentId;
        Level = level;
    }


}