using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Models.PrinterAdditions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Models
{
    public partial class Printer3d : ObservableObject, IPrinter3d, ICloneable
    {

        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        Guid calculationId;

        [ObservableProperty]
        string model = string.Empty;

        [ObservableProperty]
        Printer3dType type = Printer3dType.FDM;

        [ObservableProperty]
        Guid manufacturerId;

        [ObservableProperty]
        Manufacturer manufacturer;

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
        List<Printer3dAttribute> attributes = new();

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
        HourlyMachineRate hourlyMachineRate;

        [ObservableProperty]
        ObservableCollection<Maintenance3d> maintenances = new();

        [ObservableProperty]
        Guid slicerConfigId;

        [ObservableProperty]
        Printer3dSlicerConfig slicerConfig = new();

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
