using AndreasReitberger.Print3d.Interface;
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
        public Guid id;

        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public string debitorNumber;

        [ObservableProperty]
        public bool isActive;

        [ObservableProperty]
        public string website;

        [ObservableProperty]
        public List<Manufacturer> manufacturers = new();
        #endregion

        #region Constructor
        public Supplier() { }
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
