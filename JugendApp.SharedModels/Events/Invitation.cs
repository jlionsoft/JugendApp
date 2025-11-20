using JugendApp.SharedModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JugendApp.SharedModels.Events
{
    public class Invitation
    {
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public int PersonId { get; set; }
        public Person.Person Person { get; set; } = null!;

        public DateTime InvitedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ConfirmedAt { get; set; }
        public InvitationStatus Status { get; set; } = InvitationStatus.Pending;
    }
}
