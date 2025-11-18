using JugendApp.SharedModels.Enums;
using System.Text.RegularExpressions;
namespace JugendApp.SharedModels.Groups;



/// <summary>
/// Represents a member of a group, including their role, personal information, and the date they were added.
/// </summary>
public class GroupMember
{
    public int Id { get; set; }
    public GroupMemberRole Role { get; set; }
    public int PersonId { get; set; }
    public Person.Person? Person { get; set; }
    public int GroupId { get; private set; }
    public Group Group { get; private set; } = default!;
    public DateTime AddedAt { get; set; }
    public GroupMember() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="GroupMember"/> class with role, person, and added date.
    /// </summary>
    /// <param name="role">The <see cref="GroupMemberRole"/> assigned to the member.</param>
    /// <param name="person">The <see cref="Person"/> associated with the group member.</param>
    /// <param name="addedAt">The date and time the member was added to the group.</param>
    public GroupMember(GroupMemberRole role, Person.Person person, DateTime addedAt)
    {
        Role = role;
        Person = person;
        AddedAt = addedAt;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GroupMember"/> class with all properties including ID.
    /// </summary>
    /// <param name="id">The unique identifier of the group member.</param>
    /// <param name="role">The <see cref="GroupMemberRole"/> of the member.</param>
    /// <param name="person">The <see cref="Person"/> object representing the member.</param>
    /// <param name="addedAt">The timestamp when the member joined the group.</param>
    public GroupMember(int id, GroupMemberRole role, Person.Person person, DateTime addedAt)
    {
        Id = id;
        Role = role;
        Person = person;
        AddedAt = addedAt;
    }

    internal void AttachToGroup(Group group)
    {
        Group = group ?? throw new ArgumentNullException(nameof(group));
        GroupId = group.Id;
    }

    internal void Detach()
    {
        Group = null!;
        GroupId = 0;
    }

    public void SetRole(GroupMemberRole role) => Role = role;

}