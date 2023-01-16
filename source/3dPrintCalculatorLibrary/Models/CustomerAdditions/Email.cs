using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.CustomerAdditions
{
    public partial class Email : ObservableObject, IEmail
    {
        #region Properties
        [ObservableProperty]
        public Guid id;

        [ObservableProperty]
        public Guid customerId;

        [ObservableProperty]
        public string emailAddress;
        #endregion

        #region Constructor
        public Email()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
