using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.SQLite.PrinterAdditions;
using Newtonsoft.Json;
using SQLite;
using System.Collections.ObjectModel;
using SQLiteNetExtensions.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table("Printers")]
    public partial class Printer3dOld : ObservableObject, IPrinter3d, ICloneable
    {

        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dEnhanced))]
        Guid calculationId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dProfile))]
        Guid calculationProfileId;

        [ObservableProperty]
        string model = string.Empty;

        [ObservableProperty]
        Printer3dType type = Printer3dType.FDM;

        [ObservableProperty]
        Guid manufacturerId;

        [ObservableProperty]
        [property: ManyToOne(nameof(ManufacturerId), CascadeOperations = CascadeOperation.All)]
        Manufacturer? manufacturer;

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
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Printer3dAttribute> attributes = [];

        [ObservableProperty]
        double powerConsumption = 0;

        [ObservableProperty]
        double width = 1;

        [ObservableProperty]
        double depth = 1;

        [ObservableProperty]
        double height = 1;

        [ObservableProperty, Obsolete("No longer supported, assign a `HourlyMachineRate` instead.")]
        bool useFixedMachineHourRating = false;

        [ObservableProperty]
        Guid hourlyMachineRateId;

        [ObservableProperty]
        [property: ManyToOne(nameof(HourlyMachineRateId), CascadeOperations = CascadeOperation.All)]
        HourlyMachineRate? hourlyMachineRate;

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        ObservableCollection<Maintenance3d> maintenances = [];

        [ObservableProperty]
        Guid slicerConfigId;

        [ObservableProperty]
        [property: ManyToOne(nameof(SlicerConfigId), CascadeOperations = CascadeOperation.All)]
        Printer3dSlicerConfig slicerConfig = new();

        [ObservableProperty]
        byte[] image = [];

        [ObservableProperty]
        [property: JsonIgnore]
        string note = string.Empty;

        [JsonIgnore]
        public string Name => !string.IsNullOrEmpty(Manufacturer?.Name) ? $"{Manufacturer?.Name}, {Model}" : Model;

        [JsonIgnore]
        public double Volume => CalculateVolume();
        #endregion

        #region Constructor

        public Printer3dOld()
        {
            Id = Guid.NewGuid();
        }
        public Printer3dOld(Printer3dType type)
        {
            Id = Guid.NewGuid();
            Type = type;
        }

        #endregion

        #region Methods
        public double CalculateVolume()
        {
            return Math.Round(Width * Depth * Height, 2);
        }
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
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion

    }
}
