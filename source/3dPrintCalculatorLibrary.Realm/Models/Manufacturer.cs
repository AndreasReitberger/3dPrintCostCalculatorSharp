using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm
{
    public partial class Manufacturer : RealmObject, ICloneable, IManufacturer
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties 
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid SupplierId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        public string DebitorNumber { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public string Website { get; set; } = string.Empty;

        public string Note { get; set; } = string.Empty;

        public string CountryCode { get; set; } = string.Empty;
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
