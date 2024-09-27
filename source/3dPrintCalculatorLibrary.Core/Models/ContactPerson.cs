using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite.CustomerAdditions
{
    [Table($"{nameof(ContactPerson)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class ContactPerson : ObservableObject, IPerson
    {
        #region Properties
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

        [ObservableProperty]
        string salutation = string.Empty;

        [ObservableProperty]
        string firstName = string.Empty;

        [ObservableProperty]
        string lastName = string.Empty;

        [ObservableProperty]
        string email = string.Empty;

        [ObservableProperty]
        string phoneNumber = string.Empty;

        [ObservableProperty]
        bool showOnDocuments = true;
        #endregion

        #region Constructor
        public ContactPerson()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
