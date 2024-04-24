using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.CustomerAdditions
{
    public partial class PhoneNumber : ObservableObject, IPhoneNumber
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        Guid customerId;

        [ObservableProperty]
        string phone = string.Empty;
        #endregion

        #region Constructor
        public PhoneNumber()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
