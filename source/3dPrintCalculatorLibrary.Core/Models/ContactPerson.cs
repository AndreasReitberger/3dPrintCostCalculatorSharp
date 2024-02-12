using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Core
{
    public partial class ContactPerson : ObservableObject, IPerson
    {
        #region Properties
        [ObservableProperty]
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
