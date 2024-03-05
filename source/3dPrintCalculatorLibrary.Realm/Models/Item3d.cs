using AndreasReitberger.Print3d.Interfaces;
using Newtonsoft.Json;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm
{
    /// <summary>
    /// This is an additional item which can be defined and used for calculations.
    /// </summary>
    public partial class Item3d : RealmObject, ICloneable, IItem3d
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

        public string Name { get; set; } = string.Empty;

        public string SKU { get; set; } = string.Empty;

        public Manufacturer Manufacturer { get; set; }

        public double PackageSize { get; set; } = 1;

        public double PackagePrice { get; set; }

        public double Tax { get; set; } = 0;

        public bool PriceIncludesTax { get; set; } = true;

        public string Uri { get; set; } = string.Empty;

        public string Note { get; set; } = string.Empty;

        public string SafetyDatasheet { get; set; } = string.Empty;

        public string TechnicalDatasheet { get; set; } = string.Empty;

        [Ignored]
        public double PricePerPiece => PackagePrice / PackageSize;
        #endregion

        #region Constructor
        public Item3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Item3d item)
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
