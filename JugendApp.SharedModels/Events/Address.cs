using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace JugendApp.SharedModels.Events
{
    public class Address
    {
        public int Id { get; private set; }
        public string? Street { get; private set; }
        public string? HouseNumber { get; private set; }
        public string PostalCode { get; private set; } = "33428";
        public string City { get; private set; } = "Harsewinkel";
        public string Country { get; private set; } = "Deutschland";

        public Address() { }
        public Address(string postalCode, string city, string country, string? street = null, string? houseNumber = null)
        {
            PostalCode = postalCode;
            City = city;
            Country = country;
            Street = street;
            HouseNumber = houseNumber;
        }

        public override string ToString() => $"{Street} {HouseNumber}, {PostalCode} {City}, {Country}";

    }
}
