using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.CustomerAdditions
{
    public partial class Address : RealmObject, IAddress
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public string Supplement { get; set; } = string.Empty;

        public string Street { get; set; } = string.Empty;

        public string Zip { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string CountryCode { get; set; } = string.Empty;
        #endregion

        #region Constructor
        public Address()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
