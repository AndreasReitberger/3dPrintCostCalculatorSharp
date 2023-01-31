using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using System;

namespace AndreasReitberger.Print3d.SQLite
{
    /// <summary>
    /// This is an additional item which can be defined and used for calculations.
    /// </summary>
    [Table($"{nameof(Item3d)}s")]
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
        [property: PrimaryKey]
        public Guid id;

        [ObservableProperty]
        public string name = string.Empty;

        [ObservableProperty]
        public string sKU = string.Empty;

        [ObservableProperty]
        public double packageSize = 1;

        [ObservableProperty]
        public double packagePrice;

        [ObservableProperty]
        public double tax = 0;

        [ObservableProperty]
        public bool priceIncludesTax = true;

        [ObservableProperty]
        public string uri = string.Empty;

        [ObservableProperty]
        public string note = string.Empty;

        [ObservableProperty]
        public string safetyDatasheet = string.Empty;

        [ObservableProperty]
        public string technicalDatasheet = string.Empty;

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
        public override bool Equals(object obj)
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
