using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.CustomerAdditions
{
    public partial class PhoneNumber : ObservableObject, IPhoneNumber
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

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
