using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Xml.Serialization;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Item3d)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    /// <summary>
    /// This is an additional item which can be defined and used for calculations.
    /// </summary>
    public partial class Item3d : ObservableObject, IItem3d
    {

        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string SKU { get; set; } = string.Empty;

#if SQL
        [ObservableProperty]
        [JsonIgnore, XmlIgnore]
        public partial Guid ManufacturerId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(ManufacturerId), CascadeOperations = CascadeOperation.All)]
        public partial Manufacturer? Manufacturer { get; set; }
#else
        [ObservableProperty]
        public partial IManufacturer? Manufacturer { get; set; }
#endif

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

#if SQL
        [Ignore]
#endif
        public double PricePerPiece => PackagePrice / PackageSize;
        #endregion

        #region Constructor
        public Item3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Clone
        public object Clone() => MemberwiseClone();

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
