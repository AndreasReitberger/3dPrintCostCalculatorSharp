using AndreasReitberger.Print3d.Interface;
using AndreasReitberger.Print3d.Models.CustomerAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.Models
{
    [Table("Customers")]
    public partial class Customer3d : ObservableObject, ICloneable, ICustomer3d
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        public Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dProfile))]
        public Guid calculationProfileId;

        [ObservableProperty]
        public string customerId = string.Empty;

        [ObservableProperty]
        public bool isCompany = false;

        [ObservableProperty]
        public string salutation = string.Empty;

        [ObservableProperty]
        public string name = string.Empty;

        [ObservableProperty]
        public string lastName = string.Empty;

        [ObservableProperty]
        public string vAT = string.Empty;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        public Guid contactPersonId;

        [ObservableProperty]
        [property: ManyToOne(nameof(ContactPersonId))]
        public ContactPerson contactPerson;

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Address> addresses = new();

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Email> emailAddresses = new();

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<PhoneNumber> phoneNumbers = new();

        [ObservableProperty]
        public string handler = string.Empty;

        [JsonIgnore]
        public string FullName => IsCompany ? Name : string.Format("{0}, {1}", LastName, Name);
        
        [JsonIgnore]
        public string MainAddress => GetAddress(0);
        
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
