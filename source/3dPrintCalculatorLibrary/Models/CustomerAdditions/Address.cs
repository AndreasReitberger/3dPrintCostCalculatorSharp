using System;
using System.Collections.Generic;
using System.Text;

namespace AndreasReitberger.Models.CustomerAdditions
{
    public class Address
    {
        public string Supplement { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
    }
}
