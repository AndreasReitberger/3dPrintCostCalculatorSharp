using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.SQLite.PrinterAdditions;
using AndreasReitberger.Core.Utilities;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQLiteNetExtensions.Attributes;
using System.Xml.Serialization;
using AndreasReitberger.Print3d.Interface;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table("Printers")]
    public partial class Printer3d : ObservableObject, IPrinter3d
    {

        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        public Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3d))]
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
        [property: ManyToOne(nameof(ManufacturerId))]
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
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
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
        /*
        [JsonIgnore, XmlIgnore]
        public Guid BuildVolumeId { get; set; }

        [JsonProperty(nameof(BuildVolume))]
        BuildVolume _buildVolume = new(0, 0, 0);
        [JsonIgnore, Ignore]
        //[ManyToOne(nameof(BuildVolumeId))]
        [Obsolete("Use the x,y,z properties instead")]
        public BuildVolume BuildVolume
        {
            get { return _buildVolume; }
            set { SetProperty(ref _buildVolume, value); }
        }
        */

        [ObservableProperty]
        [property: JsonIgnore]
        bool useFixedMachineHourRating = false;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        public Guid hourlyMachineRateId;

        [ObservableProperty]
        [property: JsonIgnore]
        [property: ManyToOne(nameof(HourlyMachineRateId))]
        HourlyMachineRate hourlyMachineRate;

        [ObservableProperty]
        [property: JsonIgnore]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        ObservableCollection<Maintenance3d> maintenances = new();


        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        public Guid slicerConfigId;

        [ObservableProperty]
        [property: JsonIgnore]
        [property: ManyToOne(nameof(SlicerConfigId))]
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
        #endregion

    }
}
