using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite.CustomerAdditions
{
    [Table($"{nameof(Email)}Addresses")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Email : ObservableObject, IEmail
    {
        #region Properties
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

        [ObservableProperty]
#if SQL
        [property: ForeignKey(typeof(Customer3d))]
#endif
        Guid customerId;

        [ObservableProperty]
        string emailAddress = string.Empty;
        #endregion

        #region Constructor
        public Email()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
