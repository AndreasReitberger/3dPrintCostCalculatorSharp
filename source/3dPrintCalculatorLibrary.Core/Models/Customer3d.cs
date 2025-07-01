using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Xml.Serialization;

#if SQL

namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Customer3d)}s")]
#else
using AndreasReitberger.Print3d.Core.Interfaces;

namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Customer3d : ObservableObject, ICloneable, ICustomer3d
    {

        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ObservableProperty]
        [ForeignKey(typeof(Calculation3dProfile))]
        public partial Guid CalculationProfileId { get; set; }

        [ObservableProperty]
        [ForeignKey(typeof(ContactPerson))]
        public partial Guid ContactPersonId { get; set; }
#endif

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
#if SQL
        [ObservableProperty]
        [ManyToOne(nameof(ContactPersonId), CascadeOperations = CascadeOperation.All)]
        public partial ContactPerson? ContactPerson { get; set; }

        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Address> Addresses { get; set; } = [];

        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Email> EmailAddresses { get; set; } = [];

        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<PhoneNumber> PhoneNumbers { get; set; } = [];
#else
        [ObservableProperty]
        public partial IPerson? ContactPerson { get; set; }

        [ObservableProperty]
        public partial IList<IAddress> Addresses { get; set; } = [];

        [ObservableProperty]
        public partial IList<IEmail> EmailAddresses { get; set; } = [];

        [ObservableProperty]
        public partial IList<IPhoneNumber> PhoneNumbers { get; set; } = [];
#endif

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
#if NETSTANDARD || NET6_0_OR_GREATER
                IAddress address = index < Addresses.Count ? Addresses[index] : Addresses[^1];
#else
                IAddress address = index < Addresses.Count ? Addresses[index] : Addresses[Addresses.Count - 1];
#endif
                return $"{address?.Street}\n{(!string.IsNullOrEmpty(address?.Supplement) ? $"{address?.Supplement}\n" : "")}{address?.Zip} {address?.City}\n{address?.CountryCode}";
            }
            else
                return string.Empty;
        }
        #endregion

        #region Clone
        public object Clone() => MemberwiseClone();

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
