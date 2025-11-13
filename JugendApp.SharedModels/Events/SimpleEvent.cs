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
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person.Person CreatedBy { get; set; }
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public DateTime StartDateTime { get; set; }
        [DefaultValue(typeof(DateTime), "2025-01-01T23:00:00")]
        public DateTime EndDateTime { get; set; }

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
        public SimpleEvent()
        {
            
        }

    }
}
