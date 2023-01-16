using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table("Suppliers")]
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
        [property: PrimaryKey]
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
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
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
