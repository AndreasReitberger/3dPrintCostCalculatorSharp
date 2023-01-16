using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.CustomerAdditions
{
    public partial class ContactPerson : ObservableObject, IPerson
    {
        #region Properties
        [ObservableProperty]
        public Guid id;

        [ObservableProperty]
        public string salutation;

        [ObservableProperty]
        public string firstName;

        [ObservableProperty]
        public string lastName;

        [ObservableProperty]
        public string email;

        [ObservableProperty]
        public string phoneNumber;

        [ObservableProperty]
        public bool showOnDocuments;
        #endregion

        #region Constructor
        public ContactPerson()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
