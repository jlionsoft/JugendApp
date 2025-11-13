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
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }

        [DefaultValue("33428")]
        public string? PostalCode { get; set; }

        [DefaultValue("Harsewinkel")]
        public string? City { get; set; }

        [DefaultValue("Deutschland")]
        public string? Country { get; set; }

        public override string ToString()
        {
            return $"{Street} {HouseNumber}, {PostalCode} {City}, {Country}";
        }
    }
}
