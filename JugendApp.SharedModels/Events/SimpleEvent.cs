using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JugendApp.SharedModels.Events
{
    public class SimpleEvent
    {
        public int Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        public int PersonId { get; private set; }
        public Person.Person CreatedBy { get; private set; } = default!;

        public int LocationId { get; private set; }
        public Location Location { get; private set; } = default!;

        public DateTime StartDateTime { get; private set; }
        public DateTime EndDateTime { get; private set; }

        public SimpleEvent() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleEvent"/> class with the specified event details.
        /// </summary>
        /// <param name="title">The title of the event.</param>
        /// <param name="description">A detailed description of the event.</param>
        /// <param name="personId">The ID of the person who created the event.</param>
        /// <param name="locationId">The ID of the location where the event takes place.</param>
        /// <param name="startDateTime">The date and time when the event starts.</param>
        /// <param name="endDateTime">The date and time when the event ends.</param>
        public SimpleEvent(string title, string description, int personId, int locationId, DateTime startDateTime, DateTime endDateTime)
        {
            Title = title;
            Description = description;
            PersonId = personId;
            LocationId = locationId;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }
        public void SetEnd(DateTime end) => EndDateTime = end;


    }
}
