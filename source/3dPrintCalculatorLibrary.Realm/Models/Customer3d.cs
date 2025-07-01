using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Realm.CustomerAdditions;
using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Realm
{
    public partial class Customer3d : RealmObject, ICloneable, ICustomer3d
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid CalculationProfileId { get; set; }

        public string CustomerId { get; set; } = string.Empty;

        public bool IsCompany { get; set; } = false;

        public string Salutation { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string VAT { get; set; } = string.Empty;

        public Guid ContactPersonId { get; set; }

        public ContactPerson? ContactPerson { get; set; }

        public string Handler { get; set; } = string.Empty;

        [JsonIgnore]
        public string FullName => IsCompany ? Name : string.Format("{0}, {1}", LastName, Name);

        [JsonIgnore]
        public string MainAddress => GetAddress(0);

        #endregion

        #region Collections

        public IList<Address> Addresses { get; } = [];

        public IList<Email> EmailAddresses { get; } = [];

        public IList<PhoneNumber> PhoneNumbers { get; } = [];
        #endregion

        #region Constructor

        public Customer3d()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Methods
        public string GetAddress(int index = 0)
        {
            if (Addresses?.Count > 0)
            {
#if NETSTANDARD
                Address address = index < Addresses.Count ? Addresses[index] : Addresses[^1];
#else
                Address address = index < Addresses.Count ? Addresses[index] : Addresses[Addresses.Count - 1];
#endif
                return $"{address?.Street}\n{(!string.IsNullOrEmpty(address?.Supplement) ? $"{address?.Supplement}\n" : "")}{address?.Zip} {address?.City}\n{address?.CountryCode}";
            }
            else
                return string.Empty;
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not Customer3d item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        #endregion

    }
}
