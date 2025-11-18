using JugendApp.SharedModels.Enums;

namespace JugendApp.SharedModels.Groups;


/// <summary>
/// Represents a group with a name, description, creation date, and a list of members.
/// </summary>
public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public ICollection<GroupMember> Members { get; set; } = [];
    public Group() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Group"/> class with name, description, and creation date.
    /// </summary>
    /// <param name="name">The name of the group.</param>
    /// <param name="description">A short description of the group's purpose or focus.</param>
    /// <param name="createdAt">The date and time when the group was created.</param>
    public Group(string name, string description, DateTime createdAt)
    {
        Name = name;
        Description = description;
        CreatedAt = createdAt;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Group"/> class with all properties including ID and members.
    /// </summary>
    /// <param name="id">The unique identifier of the group.</param>
    /// <param name="name">The name of the group.</param>
    /// <param name="description">A short description of the group.</param>
    /// <param name="createdAt">The creation timestamp of the group.</param>
    /// <param name="members">A collection of <see cref="GroupMember"/>s belonging to the group.</param>
    public Group(int id, string name, string description, DateTime createdAt, ICollection<GroupMember> members)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        Members = members;
    }

    public void AddMember(Person.Person person, GroupMemberRole role, DateTime addedAt)
    {
        if (person == null) throw new ArgumentNullException(nameof(person));
        if (Members.Any(m => m.PersonId == person.Id))
        {
            // alternativ: update role/addedAt
            throw new InvalidOperationException("Person ist bereits Mitglied der Gruppe.");
        }

        var member = new GroupMember(role, person, addedAt);
        member.AttachToGroup(this);
        Members.Add(member);
    }

    public void RemoveMember(int personId)
    {
        var member = Members.SingleOrDefault(m => m.PersonId == personId);
        if (member == null) return;
        Members.Remove(member);
        member.Detach();
    }

}