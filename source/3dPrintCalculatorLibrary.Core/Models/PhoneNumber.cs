using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(PhoneNumber)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class PhoneNumber : ObservableObject, IPhoneNumber
    {
        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ForeignKey(typeof(Customer3d))]
#endif
        [ObservableProperty]
        public partial Guid CustomerId { get; set; }

        [ObservableProperty]
        public partial string Phone { get; set; } = string.Empty;
        #endregion

        #region Constructor
        public PhoneNumber()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
