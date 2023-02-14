using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Models.PrinterAdditions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Models
{
    public partial class Printer3d : ObservableObject, IPrinter3d, ICloneable
    {

        #region Properties
        [ObservableProperty]
        public Guid id;

        [ObservableProperty]
        public Guid calculationId;

        [ObservableProperty]
        [property: JsonIgnore]
        public string model = string.Empty;

        [ObservableProperty]
        [property: JsonIgnore]
        Printer3dType type = Printer3dType.FDM;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        public Guid manufacturerId;

        [ObservableProperty]
        [property: JsonIgnore]
        Manufacturer manufacturer;

        [ObservableProperty]
        [property: JsonIgnore]
        double price = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double tax = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        bool priceIncludesTax = true;

        [ObservableProperty]
        [property: JsonIgnore]
        string uri = string.Empty;

        [ObservableProperty]
        [property: JsonIgnore]
        Material3dFamily materialType = Material3dFamily.Filament;

        [ObservableProperty]
        [property: JsonIgnore]
        List<Printer3dAttribute> attributes = new();

        [ObservableProperty]
        [property: JsonIgnore]
        double powerConsumption = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double width = 1;

        [ObservableProperty]
        [property: JsonIgnore]
        double depth = 1;

        [ObservableProperty]
        [property: JsonIgnore]
        double height = 1;

        [ObservableProperty]
        [property: JsonIgnore]
        bool useFixedMachineHourRating = false;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        public Guid hourlyMachineRateId;

        [ObservableProperty]
        [property: JsonIgnore]
        HourlyMachineRate hourlyMachineRate;

        [ObservableProperty]
        [property: JsonIgnore]
        ObservableCollection<Maintenance3d> maintenances = new();


        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        public Guid slicerConfigId;

        [ObservableProperty]
        [property: JsonIgnore]
        Printer3dSlicerConfig slicerConfig = new();

        [ObservableProperty]
        [property: JsonIgnore]
        byte[] image = Array.Empty<byte>();

        [ObservableProperty]
        [property: JsonIgnore]
        public string note = string.Empty;

        [JsonIgnore]
        public string Name => (Manufacturer != null && !string.IsNullOrEmpty(Manufacturer.Name)) ? string.Format("{0}, {1}", Manufacturer.Name, Model) : Model;

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
        public override bool Equals(object obj)
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
