using AndreasReitberger.Enums;
using AndreasReitberger.Models.PrinterAdditions;
using AndreasReitberger.Core.Utilities;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQLiteNetExtensions.Attributes;
using System.Xml.Serialization;

namespace AndreasReitberger.Models
{
    [Table("Printers")]
    public class Printer3d : BaseModel
    {

        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(Calculation3d))]
        public Guid CalculationId { get; set; }

        [JsonProperty(nameof(Model)), XmlIgnore]
        public string _model = string.Empty;
        [JsonIgnore]
        public string Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        [JsonProperty(nameof(Type))]
        Printer3dType _type = Printer3dType.FDM;
        [JsonIgnore]
        public Printer3dType Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }

        [JsonIgnore, XmlIgnore]
        public Guid ManufacturerId { get; set; }
        
        [JsonProperty(nameof(Manufacturer))]
        Manufacturer manufacturer;
        [JsonIgnore]
        [ManyToOne(nameof(ManufacturerId))]
        public Manufacturer Manufacturer
        {
            get { return manufacturer; }
            set { SetProperty(ref manufacturer, value); }
        }

        [JsonProperty(nameof(Price))]
        double _price = 0;
        [JsonIgnore]
        public double Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

        [JsonProperty(nameof(Tax))]
        double _tax = 0;
        [JsonIgnore]
        public double Tax
        {
            get { return _tax; }
            set { SetProperty(ref _tax, value); }
        }

        [JsonProperty(nameof(PriceIncludesTax))]
        bool _priceIncludesTax = true;
        [JsonIgnore]
        public bool PriceIncludesTax
        {
            get { return _priceIncludesTax; }
            set { SetProperty(ref _priceIncludesTax, value); }
        }

        [JsonProperty(nameof(Uri))]
        string _uri = string.Empty;
        [JsonIgnore]
        public string Uri
        {
            get { return _uri; }
            set { SetProperty(ref _uri, value); }
        }

        [JsonProperty(nameof(MaterialType))]
        Material3dFamily _materialType = Material3dFamily.Filament;
        [JsonIgnore]
        public Material3dFamily MaterialType
        {
            get { return _materialType; }
            set { SetProperty(ref _materialType, value); }
        }

        [JsonProperty(nameof(Attributes))]
        List<Printer3dAttribute> _attributes = new();
        [JsonIgnore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Printer3dAttribute> Attributes
        {
            get { return _attributes; }
            set { SetProperty(ref _attributes, value); }
        }

        [JsonProperty(nameof(PowerConsumption))]
        double _powerConsumption = 0;
        [JsonIgnore]
        public double PowerConsumption
        {
            get { return _powerConsumption; }
            set { SetProperty(ref _powerConsumption, value); }
        }

        [JsonProperty(nameof(Width))]
        double _width = 1;
        [JsonIgnore]
        public double Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }

        [JsonProperty(nameof(Depth))]
        double _depth = 1;
        [JsonIgnore]
        public double Depth
        {
            get { return _depth; }
            set { SetProperty(ref _depth, value); }
        }

        [JsonProperty(nameof(Height))]
        double _height = 1;
        [JsonIgnore]
        public double Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }
        /**/
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
        

        [JsonProperty(nameof(UseFixedMachineHourRating))]
        bool _useFixedMachineHourRating = false;
        [JsonIgnore]
        public bool UseFixedMachineHourRating
        {
            get { return _useFixedMachineHourRating; }
            set { SetProperty(ref _useFixedMachineHourRating, value); }
        }

        [JsonIgnore, XmlIgnore]
        public Guid HourlyMachineRateId { get; set; }

        [JsonProperty(nameof(HourlyMachineRate))]
        HourlyMachineRate _hourlyMachineRate;
        [JsonIgnore]
        [ManyToOne(nameof(HourlyMachineRateId))]
        public HourlyMachineRate HourlyMachineRate
        {
            get { return _hourlyMachineRate; }
            set { SetProperty(ref _hourlyMachineRate, value); }
        }

        [JsonProperty(nameof(Maintenances))]
        ObservableCollection<Maintenance3d> _maintenances = new();
        [JsonIgnore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Maintenance3d> Maintenances
        {
            get { return _maintenances; }
            set { SetProperty(ref _maintenances, value); }
        }

        [JsonIgnore, XmlIgnore]
        public Guid SlicerConfigId { get; set; }

        [JsonProperty(nameof(SlicerConfig))]
        Printer3dSlicerConfig _slicerConfig = new();
        [JsonIgnore]
        [ManyToOne(nameof(SlicerConfigId))]
        public Printer3dSlicerConfig SlicerConfig
        {
            get { return _slicerConfig; }
            set { SetProperty(ref _slicerConfig, value); }
        }

        [JsonProperty(nameof(Image))]
        byte[] _image = new byte[0];
        [JsonIgnore]
        public byte[] Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        [JsonProperty(nameof(Note))]
        public string _note = string.Empty;
        [JsonIgnore]
        public string Note
        {
            get { return _note; }
            set { SetProperty(ref _note, value); }
        }

        [JsonIgnore]
        public string Name
        {
            get => (Manufacturer != null && !string.IsNullOrEmpty(Manufacturer.Name)) ? string.Format("{0}, {1}", Manufacturer.Name, Model) : Model;
        }
        [JsonIgnore]
        public double Volume
        {
            get => CalculateVolume();
        }
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
