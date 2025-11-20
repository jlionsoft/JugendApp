using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace JugendApp.DTOs
{
    public class InvitationDto
    {
        public int EventId { get; set; }
        public int PersonId { get; set; }

        public DateTime InvitedAt { get; set; }
        public DateTime? ConfirmedAt { get; set; }
        public InvitationStatus Status { get; set; }
    }
    public enum InvitationStatus
    {
        Pending,
        Accepted,
        Declined
    }
}
