using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Models
{
    public partial class Supplier : ObservableObject, ICloneable, ISupplier
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties 
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string debitorNumber;

        [ObservableProperty]
        bool isActive;

        [ObservableProperty]
        string website;

        #endregion

        #region Collections

        [ObservableProperty]
        List<Manufacturer> manufacturers = new();
        #endregion

        #region Constructor
        public Supplier() {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return string.IsNullOrEmpty(DebitorNumber) ? Name : string.Format("{0} ({1})", Name, DebitorNumber);
        }
        public override bool Equals(object obj)
        {
            if (obj is not Supplier item)
                return false;
            return this.Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        #endregion
    }
}
