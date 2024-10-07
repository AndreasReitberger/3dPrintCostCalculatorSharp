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
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

#if SQL
        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dEnhanced))]
        Guid calculationId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dProfile))]
        Guid calculationProfileId;
#endif

        [ObservableProperty]
        string model = string.Empty;

        [ObservableProperty]
        Printer3dType type = Printer3dType.FDM;

#if SQL
        [ObservableProperty]
        Guid manufacturerId;

        [ObservableProperty]
        [property: ManyToOne(nameof(ManufacturerId), CascadeOperations = CascadeOperation.All)]
        Manufacturer? manufacturer;
#else
        [ObservableProperty]
        IManufacturer? manufacturer;
#endif

        [ObservableProperty]
        double price = 0;

        [ObservableProperty]
        double tax = 0;

        [ObservableProperty]
        bool priceIncludesTax = true;

        [ObservableProperty]
        string uri = string.Empty;

        [ObservableProperty]
        Material3dFamily materialType = Material3dFamily.Filament;

        [ObservableProperty]
        double powerConsumption = 0;

        [ObservableProperty]
        double width = 1;

        [ObservableProperty]
        double depth = 1;

        [ObservableProperty]
        double height = 1;

#if SQL
        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Printer3dAttribute> attributes = [];

        [ObservableProperty]
        Guid hourlyMachineRateId;

        [ObservableProperty]
        [property: ManyToOne(nameof(HourlyMachineRateId), CascadeOperations = CascadeOperation.All)]
        HourlyMachineRate? hourlyMachineRate;

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Maintenance3d> maintenances = [];

        [ObservableProperty]
        Guid slicerConfigId;

        [ObservableProperty]
        [property: ManyToOne(nameof(SlicerConfigId), CascadeOperations = CascadeOperation.All)]
        Printer3dSlicerConfig slicerConfig =
#if NET6_0_OR_GREATER
            (Printer3dSlicerConfig)Printer3dSlicerConfig.Default;
#else
            new();
#endif
#else
        [ObservableProperty]
        IList<IPrinter3dAttribute> attributes = [];

        [ObservableProperty]
        IHourlyMachineRate? hourlyMachineRate;

        [ObservableProperty]
        IList<IMaintenance3d> maintenances = [];

        [ObservableProperty]
        IPrinter3dSlicerConfig? slicerConfig =
#if NET6_0_OR_GREATER
            IPrinter3dSlicerConfig.Default;
#else
            new Printer3dSlicerConfig();
#endif
#endif
        [ObservableProperty]
        byte[] image = [];

        [ObservableProperty]
        string note = string.Empty;

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
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Printer3d item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public object Clone() => MemberwiseClone();
        
        #endregion

    }
}
