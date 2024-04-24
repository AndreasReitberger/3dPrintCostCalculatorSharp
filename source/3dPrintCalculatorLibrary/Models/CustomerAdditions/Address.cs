using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.CustomerAdditions
{
    public partial class Address : ObservableObject, IAddress
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        Guid customerId;

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
