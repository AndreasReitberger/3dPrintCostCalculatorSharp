using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.CustomerAdditions
{
    public partial class ContactPerson : ObservableObject, IPerson
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Salutation { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string FirstName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string LastName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Email { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string PhoneNumber { get; set; } = string.Empty;

        [ObservableProperty]
        public partial bool ShowOnDocuments { get; set; }
        #endregion

        #region Constructor
        public ContactPerson()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
