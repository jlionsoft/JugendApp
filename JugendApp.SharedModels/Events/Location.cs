using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JugendApp.SharedModels.Events
{
    public class Location
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int AddressId { get; set; }
        
        [ForeignKey("AddressId")]
        public Address Address { get; set; }

    }
}
