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
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid CalculationId { get; set; }

        [ObservableProperty]
        public partial string Model { get; set; } = string.Empty;

        [ObservableProperty]
        public partial Printer3dType Type { get; set; } = Printer3dType.FDM;

        [ObservableProperty]
        public partial Guid ManufacturerId { get; set; }

        [ObservableProperty]
        public partial Manufacturer? Manufacturer { get; set; }

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
        public partial List<Printer3dAttribute> Attributes { get; set; } = [];

        [ObservableProperty]
        public partial double PowerConsumption { get; set; } = 0;

        [ObservableProperty]
        public partial double Width { get; set; } = 1;

        [ObservableProperty]
        public partial double Depth { get; set; } = 1;

        [ObservableProperty]
        public partial double Height { get; set; } = 1;

        [ObservableProperty]
        [field: Obsolete("No longer supported, assign a `HourlyMachineRate` instead.")]
        public partial bool UseFixedMachineHourRating { get; set; } = false;

        [ObservableProperty]
        public partial Guid HourlyMachineRateId { get; set; }

        [ObservableProperty]
        public partial HourlyMachineRate? HourlyMachineRate { get; set; }

        [ObservableProperty]
        public partial ObservableCollection<Maintenance3d> Maintenances { get; set; } = [];

        [ObservableProperty]
        public partial Guid SlicerConfigId { get; set; }

        [ObservableProperty]
        public partial Printer3dSlicerConfig SlicerConfig { get; set; } = new();

        [ObservableProperty]
        public partial byte[] Image { get; set; } = [];

        [ObservableProperty]
        public partial string Note { get; set; } = string.Empty;

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
