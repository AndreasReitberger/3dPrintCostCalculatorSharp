using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.Print3d.Core
{
    public partial class Printer3d : ObservableObject, IPrinter3d
    {

        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string model = string.Empty;

        [ObservableProperty]
        Printer3dType type = Printer3dType.FDM;

        [ObservableProperty]
        IManufacturer? manufacturer;

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
        IList<IPrinter3dAttribute> attributes = [];

        [ObservableProperty]
        double powerConsumption = 0;

        [ObservableProperty]
        double width = 1;

        [ObservableProperty]
        double depth = 1;

        [ObservableProperty]
        double height = 1;

        [ObservableProperty]
        IHourlyMachineRate? hourlyMachineRate;

        [ObservableProperty]
        IList<IMaintenance3d> maintenances = [];

        [ObservableProperty]
        Guid slicerConfigId;

        [ObservableProperty]
        IPrinter3dSlicerConfig? slicerConfig;

        [ObservableProperty]
        byte[] image = [];

        [ObservableProperty]
        string note = string.Empty;

        [JsonIgnore]
        public string Name => !string.IsNullOrEmpty(Manufacturer?.Name) ? $"{Manufacturer?.Name}, {Model}" : Model;

        [JsonIgnore]
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
