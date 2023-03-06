using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.SQLite.CustomerAdditions
{
    [Table("Addresses")]
    public partial class Address : ObservableObject, IAddress
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Customer3d))]
        Guid customerId;

        [ObservableProperty]
        string supplement;

        [ObservableProperty]
        string street;

        [ObservableProperty]
        string zip;

        [ObservableProperty]
        string city;

        [ObservableProperty]
        string countryCode;
        #endregion

        #region Constructor
        public Address()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
