using AndreasReitberger.Print3d.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.Models.CustomerAdditions
{
    [Table("Addresses")]
    public partial class Address : ObservableObject, IAddress
    {
        #region Properties
        [ObservableProperty]
        
        public Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Customer3d))]
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
