using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.CustomerAdditions
{
    public partial class Address : ObservableObject, IAddress
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid CustomerId { get; set; }

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
