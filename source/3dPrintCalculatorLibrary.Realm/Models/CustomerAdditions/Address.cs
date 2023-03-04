using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Realm.CustomerAdditions
{
    public partial class Address : ObservableObject, IAddress
    {
        #region Properties
        [ObservableProperty]
        public Guid id;

        [ObservableProperty]
        public Guid customerId;

        [ObservableProperty]
        public string supplement;

        [ObservableProperty]
        public string street;

        [ObservableProperty]
        public string zip;

        [ObservableProperty]
        public string city;

        [ObservableProperty]
        public string countryCode;
        #endregion

        #region Constructor
        public Address()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
