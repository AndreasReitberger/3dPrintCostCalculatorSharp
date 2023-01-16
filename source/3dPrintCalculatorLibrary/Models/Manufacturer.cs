using AndreasReitberger.Print3d.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.Models
{
    [Table("Manufacturers")]
    public partial class Manufacturer : ObservableObject, ICloneable, IManufacturer
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
        [property: ForeignKey(typeof(Supplier))]
        public Guid supplierId;

        [ObservableProperty]
        public string name = string.Empty;

        [ObservableProperty]
        public string debitorNumber = string.Empty;

        [ObservableProperty]
        public bool isActive = true; 

        [ObservableProperty]
        public string website = string.Empty;

        [ObservableProperty]
        public string note = string.Empty;

        [ObservableProperty]
        public string countryCode = string.Empty;
        #endregion

        #region Constructor
        public Manufacturer()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return string.IsNullOrEmpty(DebitorNumber) ? Name : $"{Name} ({DebitorNumber})";
        }
        public override bool Equals(object obj)
        {
            if (obj is not Manufacturer item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}
