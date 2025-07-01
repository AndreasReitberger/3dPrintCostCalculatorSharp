using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Print3d.Models
{
    /// <summary>
    /// This is an additional item which can be defined and used for calculations.
    /// </summary>
    public partial class Item3d : ObservableObject, ICloneable, IItem3d
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string SKU { get; set; } = string.Empty;

        [ObservableProperty]
        public partial Guid ManufacturerId { get; set; }

        [ObservableProperty]
        public partial Manufacturer? Manufacturer { get; set; }

        [ObservableProperty]
        public partial double PackageSize { get; set; } = 1;

        [ObservableProperty]
        public partial double PackagePrice { get; set; }

        [ObservableProperty]
        public partial double Tax { get; set; } = 0;

        [ObservableProperty]
        public partial bool PriceIncludesTax { get; set; } = true;

        [ObservableProperty]
        public partial string Uri { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Note { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string SafetyDatasheet { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string TechnicalDatasheet { get; set; } = string.Empty;

        public double PricePerPiece => PackagePrice / PackageSize;
        #endregion

        #region Constructor
        public Item3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not Item3d item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}
