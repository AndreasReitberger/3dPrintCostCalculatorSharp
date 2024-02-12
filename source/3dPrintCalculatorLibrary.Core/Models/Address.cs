using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Core
{
    public partial class Address : ObservableObject, IAddress
    {
        #region Properties
        [ObservableProperty]
        Guid id;

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
