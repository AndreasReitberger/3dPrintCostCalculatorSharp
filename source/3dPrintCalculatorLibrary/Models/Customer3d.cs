using AndreasReitberger.Print3d.Models.CustomerAdditions;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.Models
{
    [Table("Customers")]
    public class Customer3d : ICloneable
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

        [ForeignKey(typeof(Calculation3dProfile))]
        public Guid CalculationProfileId { get; set; }

        public string CustomerId
        { get; set; } = string.Empty;

        public bool IsCompany
        { get; set; } = false;
        public string Salutation
        { get; set; } = string.Empty;

        public string Name
        { get; set; } = string.Empty;

        public string LastName
        { get; set; } = string.Empty;
        public string VAT
        { get; set; } = string.Empty;

        [JsonIgnore, XmlIgnore]
        public Guid ContactPersonId { get; set; }
        [ManyToOne(nameof(ContactPersonId))]
        public ContactPerson ContactPerson
        { get; set; }

        //[ManyToMany(typeof(Address))]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Address> Addresses { get; set; } = new List<Address>();

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Email> EmailAddresses { get; set; } = new();
        //public List<string> EmailAddresses { get; set; } = new List<string>();

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<PhoneNumber> PhoneNumbers { get; set; } = new();
        //public List<string> PhoneNumbers { get; set; } = new List<string>();
        
        public string Handler
        { get; set; } = string.Empty;

        [JsonIgnore]
        public string FullName
        {
            get => IsCompany ? Name : string.Format("{0}, {1}", LastName, Name);
        }

        [JsonIgnore]
        public string MainAddress
        {
            get => GetAddress(0);
        }
        #endregion

        #region Constructor

        public Customer3d()
        {
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
        public override string ToString()
        {
            return IsCompany ? Name : string.Format("{0}, {1}", LastName, Name);
        }
        public override bool Equals(object obj)
        {
            if (obj is not Customer3d item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion

    }
}
