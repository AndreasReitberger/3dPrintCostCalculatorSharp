using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Models.CustomerAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.Models
{
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
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid CalculationProfileId { get; set; }

        [ObservableProperty]
        public partial string CustomerId { get; set; } = string.Empty;

        [ObservableProperty]
        public partial bool IsCompany { get; set; } = false;

        [ObservableProperty]
        public partial string Salutation { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string LastName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string VAT { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonIgnore, XmlIgnore]
        public partial Guid ContactPersonId { get; set; }

        [ObservableProperty]
        public partial ContactPerson? ContactPerson { get; set; }

        [ObservableProperty]
        public partial List<Address> Addresses { get; set; } = [];

        [ObservableProperty]
        public partial List<Email> EmailAddresses { get; set; } = [];

        [ObservableProperty]
        public partial List<PhoneNumber> PhoneNumbers { get; set; } = [];

        [ObservableProperty]
        public partial string Handler { get; set; } = string.Empty;

        [JsonIgnore]
        public string FullName => IsCompany ? Name : $"{LastName}, {Name}";

        [JsonIgnore]
        public string MainAddress => GetAddress(0);

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
