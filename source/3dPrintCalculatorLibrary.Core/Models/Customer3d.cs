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
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

#if SQL
        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dProfile))]
        Guid calculationProfileId;

        [ObservableProperty]
        Guid contactPersonId;
#endif

        [ObservableProperty]
        string customerId = string.Empty;

        [ObservableProperty]
        bool isCompany = false;

        [ObservableProperty]
        string salutation = string.Empty;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        string lastName = string.Empty;

        [ObservableProperty]
        string vAT = string.Empty;
#if SQL
        [ObservableProperty]
        [property: ManyToOne(nameof(ContactPersonId), CascadeOperations = CascadeOperation.All)]
        ContactPerson? contactPerson;

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Address> addresses = [];

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Email> emailAddresses = [];

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<PhoneNumber> phoneNumbers = [];
#else
        [ObservableProperty]
        IPerson? contactPerson;

        [ObservableProperty]
        IList<IAddress> addresses = [];

        [ObservableProperty]
        IList<IEmail> emailAddresses = [];

        [ObservableProperty]
        IList<IPhoneNumber> phoneNumbers = [];
#endif

        [ObservableProperty]
        string handler = string.Empty;

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
        public override string ToString()
        {
            return IsCompany ? Name : string.Format("{0}, {1}", LastName, Name);
        }
        public override bool Equals(object? obj)
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
