using AndreasReitberger.Enums;
using AndreasReitberger.Models.PrinterAdditions;
using AndreasReitberger.Core.Utilities;
using Newtonsoft.Json;
//using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AndreasReitberger.Models
{
    public class Printer3d : BaseModel
    {

        #region Properties
        //[PrimaryKey]
        public Guid Id { get; set; }

        [JsonProperty(nameof(Model))]
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

        [JsonProperty(nameof(Manufacturer))]
        Manufacturer manufacturer;
        [JsonIgnore]
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

        [JsonProperty(nameof(BuildVolume))]
        BuildVolume _buildVolume = new(0, 0, 0);
        [JsonIgnore]
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

        [JsonProperty(nameof(HourlyMachineRate))]
        HourlyMachineRate _hourlyMachineRate;
        [JsonIgnore]
        public HourlyMachineRate HourlyMachineRate
        {
            get { return _hourlyMachineRate; }
            set { SetProperty(ref _hourlyMachineRate, value); }
        }

        [JsonProperty(nameof(Maintenances))]
        ObservableCollection<Maintenance3d> _maintenances = new();
        [JsonIgnore]
        public ObservableCollection<Maintenance3d> Maintenances
        {
            get { return _maintenances; }
            set { SetProperty(ref _maintenances, value); }
        }

        [JsonProperty(nameof(SlicerConfig))]
        Printer3dSlicerConfig _slicerConfig = new();
        [JsonIgnore]
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

        [JsonIgnore]
        public string Name
        {
            get => (Manufacturer != null && !string.IsNullOrEmpty(Manufacturer.Name)) ? string.Format("{0}, {1}", Manufacturer.Name, this.Model) : this.Model;
        }
        #endregion

        #region Constructor

        public Printer3d()
        {
        }
        public Printer3d(Printer3dType Type)
        {
            this.Type = Type;
        }

        #endregion

        #region overrides
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is not Printer3d item)
                return false;
            return this.Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        #endregion

    }
}
