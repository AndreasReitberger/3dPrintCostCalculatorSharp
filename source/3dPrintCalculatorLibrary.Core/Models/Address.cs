using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
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
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ForeignKey(typeof(Customer3d))]
        [ObservableProperty]
        public partial Guid CustomerId { get; set; }
#endif
        [ObservableProperty]
        public partial string Supplement { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Street { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Zip { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string City { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string CountryCode { get; set; } = string.Empty;
        #endregion

        #region Constructor
        public Address()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
