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
        public int Id { get; private set; }
        public string? Name { get; private set; }

        public int AddressId { get; private set; }
        public Address Address { get; private set; } = default!;

        public Location() { }
        public Location(Address address, string? name = null)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
            AddressId = address.Id;
            Name = name;
        }


    }
}
