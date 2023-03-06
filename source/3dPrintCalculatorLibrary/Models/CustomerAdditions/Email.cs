using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.CustomerAdditions
{
    public partial class Email : ObservableObject, IEmail
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        Guid customerId;

        [ObservableProperty]
        string emailAddress;
        #endregion

        #region Constructor
        public Email()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
