using CommunityToolkit.Mvvm.ComponentModel;
#if SQL
using SQLite;
using SQLiteNetExtensions.Attributes;
#endif

#if SQL
namespace AndreasReitberger.Print3d.SQLite.CustomerAdditions
{
    [Table($"{nameof(Address)}es")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Address : ObservableObject, IAddress
    {
        #region Properties
#if SQL
        [property: PrimaryKey]
#endif
        [ObservableProperty]
        Guid id;

#if SQL
        [ObservableProperty]
        [property: ForeignKey(typeof(Customer3d))]
        Guid customerId;
#endif
        [ObservableProperty]
        string supplement = string.Empty;

        [ObservableProperty]
        string street = string.Empty;

        [ObservableProperty]
        string zip = string.Empty;

        [ObservableProperty]
        string city = string.Empty;

        [ObservableProperty]
        string countryCode = string.Empty;
        #endregion

        #region Constructor
        public Address()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
