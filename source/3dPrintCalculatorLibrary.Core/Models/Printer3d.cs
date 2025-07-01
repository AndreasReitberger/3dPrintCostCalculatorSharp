using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Printer3d)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Printer3d : ObservableObject, IPrinter3d
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
        public partial string Model { get; set; } = string.Empty;

        [ObservableProperty]
        public partial Printer3dType Type { get; set; } = Printer3dType.FDM;

#if SQL
        [ObservableProperty]
        public partial Guid ManufacturerId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(ManufacturerId), CascadeOperations = CascadeOperation.All)]
        public partial Manufacturer? Manufacturer { get; set; }
#else
        [ObservableProperty]
        public partial IManufacturer? Manufacturer { get; set; }
#endif

        [ObservableProperty]
        public partial double Price { get; set; } = 0;

        [ObservableProperty]
        public partial double Tax { get; set; } = 0;

        [ObservableProperty]
        public partial bool PriceIncludesTax { get; set; } = true;

        [ObservableProperty]
        public partial string Uri { get; set; } = string.Empty;

        [ObservableProperty]
        public partial Material3dFamily MaterialType { get; set; } = Material3dFamily.Filament;

        [ObservableProperty]
        public partial double PowerConsumption { get; set; } = 0;

        [ObservableProperty]
        public partial double Width { get; set; } = 1;

        [ObservableProperty]
        public partial double Depth { get; set; } = 1;

        [ObservableProperty]
        public partial double Height { get; set; } = 1;

#if SQL
        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Printer3dAttribute> Attributes { get; set; } = [];

        [ObservableProperty]
        public partial Guid HourlyMachineRateId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(HourlyMachineRateId), CascadeOperations = CascadeOperation.All)]
        public partial HourlyMachineRate? HourlyMachineRate { get; set; }

        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Maintenance3d> Maintenances { get; set; } = [];

        [ObservableProperty]
        public partial Guid SlicerConfigId { get; set; }
        [ObservableProperty]
        [ManyToOne(nameof(SlicerConfigId), CascadeOperations = CascadeOperation.All)]
        public partial Printer3dSlicerConfig? SlicerConfig { get; set; } =
#if NET6_0_OR_GREATER
            (Printer3dSlicerConfig)Printer3dSlicerConfig.Default;
#else
            new();
#endif
#else
        [ObservableProperty]
        public partial IList<IPrinter3dAttribute> Attributes { get; set; } = [];

        [ObservableProperty]
        public partial IHourlyMachineRate? HourlyMachineRate { get; set; }

        [ObservableProperty]
        public partial IList<IMaintenance3d> Maintenances { get; set; } = [];
        [ObservableProperty]
        public partial IPrinter3dSlicerConfig? SlicerConfig { get; set; } =
#if NET6_0_OR_GREATER
            IPrinter3dSlicerConfig.Default;
#else
            new Printer3dSlicerConfig();
#endif
#endif
        [ObservableProperty]
        public partial byte[] Image { get; set; } = [];

        [ObservableProperty]
        public partial string Note { get; set; } = string.Empty;

        [JsonIgnore]
#if SQL
        [Ignore]
#endif
        public string Name => !string.IsNullOrEmpty(Manufacturer?.Name) ? $"{Manufacturer?.Name}, {Model}" : Model;

        [JsonIgnore]
#if SQL
        [Ignore]
#endif
        public double Volume => CalculateVolume();
#endregion

        #region Constructor

        public Printer3d()
        {
            Id = Guid.NewGuid();
        }
        public Printer3d(Printer3dType type)
        {
            Id = Guid.NewGuid();
            Type = type;
        }

        #endregion

        #region Methods
        public double CalculateVolume() => Math.Round(Width * Depth * Height, 2);

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not Printer3d item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();
        
        public object Clone() => MemberwiseClone();
        
        #endregion

    }
}
