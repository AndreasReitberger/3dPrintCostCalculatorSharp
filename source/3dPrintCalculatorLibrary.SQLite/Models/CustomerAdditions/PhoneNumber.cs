﻿using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.SQLite.CustomerAdditions
{
    [Table("PhoneNumbers")]
    public partial class PhoneNumber : ObservableObject, IPhoneNumber
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        public Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Customer3d))]
        public Guid customerId;

        [ObservableProperty]
        public string phone;
        #endregion

        #region Constructor
        public PhoneNumber()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
