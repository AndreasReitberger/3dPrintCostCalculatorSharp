using AndreasReitberger.Print3d.Interfaces;
using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Realm
{
    public partial class Supplier : RealmObject, ICloneable, ISupplier
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
        [Required]
        public string Name { get; set; } = string.Empty;

        public string DebitorNumber { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public string Website { get; set; } = string.Empty;

        #endregion

        #region Collections
        public IList<Manufacturer> Manufacturers { get; } = [];
        #endregion

        #region Constructor
        public Supplier() { }
        #endregion

        #region Override
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not Supplier item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}
