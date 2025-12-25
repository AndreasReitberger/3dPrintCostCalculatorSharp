using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Xml.Serialization;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Material3d)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Material3d : ObservableObject, IMaterial3d
    {
        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ObservableProperty]
        [ForeignKey(typeof(Calculation3dEnhanced))]
        public partial Guid CalculationId { get; set; }

        [ObservableProperty]
        [ForeignKey(typeof(Calculation3dProfile))]
        public partial Guid CalculationProfileId { get; set; }
#endif

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string SKU { get; set; } = string.Empty;

        [ObservableProperty]
        public partial Unit Unit { get; set; } = Unit.Kilogram;

        [ObservableProperty]
        public partial double PackageSize { get; set; } = 1;

        [ObservableProperty]
        public partial double Density { get; set; } = 1;

        [ObservableProperty]
        public partial double FactorLToKg { get; set; } = 1;

#if SQL
        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Material3dAttribute> Attributes { get; set; } = [];

        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Material3dProcedureAttribute> ProcedureAttributes { get; set; } = [];

        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Material3dColor> Colors { get; set; } = [];

        [ObservableProperty]
        [JsonIgnore, XmlIgnore]
        [ForeignKey(typeof(Material3dType))]
        public partial Guid MaterialTypeId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(MaterialTypeId), CascadeOperations = CascadeOperation.All)]
        public partial Material3dType? TypeOfMaterial { get; set; }

        [ObservableProperty]
        [JsonIgnore, XmlIgnore]
        [ForeignKey(typeof(Manufacturer))]
        public partial Guid ManufacturerId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(ManufacturerId), CascadeOperations = CascadeOperation.All)]
        public partial Manufacturer? Manufacturer { get; set; }
#else
        [ObservableProperty]
        public partial IList<IMaterial3dAttribute> Attributes { get; set; } = [];

        [ObservableProperty]
        public partial IList<IMaterial3dProcedureAttribute> ProcedureAttributes { get; set; } = [];

        [ObservableProperty]
        public partial IList<IMaterial3dColor> Colors { get; set; } = [];

        [ObservableProperty]
        public partial IMaterial3dType? TypeOfMaterial { get; set; }

        [ObservableProperty]
        public partial IManufacturer? Manufacturer { get; set; }
#endif

        [ObservableProperty]
        public partial Material3dFamily MaterialFamily { get; set; } = Material3dFamily.Filament;

        [ObservableProperty]
        public partial double UnitPrice { get; set; }

        [ObservableProperty]
        public partial double Tax { get; set; } = 0;

        [ObservableProperty]
        public partial bool PriceIncludesTax { get; set; } = true;

        [ObservableProperty]
        public partial string Uri { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string ColorCode { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Note { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string SafetyDatasheet { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string TechnicalDatasheet { get; set; } = string.Empty;

        [ObservableProperty]
        public partial Unit SpoolWeightUnit { get; set; } = Unit.Gram;

        [ObservableProperty]
        public partial double SpoolWeight { get; set; } = 200;

        [ObservableProperty]
        public partial byte[] Image { get; set; } = [];
        #endregion

        #region Constructor
        public Material3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Clone
        public object Clone() => MemberwiseClone();
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.Material3d);
        public override bool Equals(object? obj)
        {
            if (obj is not Material3d item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}
