using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Xml.Serialization;

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
        Guid id;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        string sKU = string.Empty;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        Guid manufacturerId;

        [ObservableProperty]
        [property: ManyToOne(nameof(ManufacturerId))]
        Manufacturer manufacturer;

        [ObservableProperty]
        double packageSize = 1;

        [ObservableProperty]
        double packagePrice;

        [ObservableProperty]
        double tax = 0;

        [ObservableProperty]
        bool priceIncludesTax = true;

        [ObservableProperty]
        string uri = string.Empty;

        [ObservableProperty]
        string note = string.Empty;

        [ObservableProperty]
        string safetyDatasheet = string.Empty;

        [ObservableProperty]
        string technicalDatasheet = string.Empty;

        [Ignore]
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
