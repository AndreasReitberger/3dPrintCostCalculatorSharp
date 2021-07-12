using System;
using System.Collections.Generic;
using System.Text;

namespace AndreasReitberger.Models.CustomerAdditions
{
    public class ContactPerson
    {
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool showOnDocuments { get; set; }
    }
}
