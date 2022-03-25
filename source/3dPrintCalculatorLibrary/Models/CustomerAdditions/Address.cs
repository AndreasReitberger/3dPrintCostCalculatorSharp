using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Models.CustomerAdditions
{
    [Table("Addresses")]
    public class Address
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(Customer3d))]
        public Guid CustomerId { get; set; }
        public string Supplement { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        #endregion

        #region Constructor
        public Address()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
