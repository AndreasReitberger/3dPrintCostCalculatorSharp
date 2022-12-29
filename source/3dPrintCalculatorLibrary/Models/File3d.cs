﻿using AndreasReitberger.Print3d.Models.FileAdditions;
using AndreasReitberger.Core.Utilities;
using Newtonsoft.Json;
using SQLite;
using System;
using SQLiteNetExtensions.Attributes;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.Models
{
    [Table("Files")]
    public class File3d : BaseModel
    {
        #region Properties
        [PrimaryKey]
        public Guid Id
        { get; set; }

        [ForeignKey(typeof(Calculation3d))]
        public Guid CalculationId { get; set; }

        [JsonProperty(nameof(Name))]
        string _name = string.Empty;
        [JsonIgnore]
        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        [JsonProperty(nameof(File))]
        object _file;
        [JsonIgnore, Ignore]
        public object File
        {
            get { return _file; }
            set
            {
                SetProperty(ref _file, value);
            }
        }

        [JsonProperty(nameof(FileName))]
        string _fileName = string.Empty;
        [JsonIgnore]
        public string FileName
        {
            get { return _fileName; }
            set
            {
                SetProperty(ref _fileName, value);
            }
        }

        [JsonProperty(nameof(FilePath))]
        string _filePath = string.Empty;
        [JsonIgnore]
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                SetProperty(ref _filePath, value);
            }
        }

        [JsonProperty(nameof(Volume))]
        double _volume = 0;
        [JsonIgnore]
        public double Volume
        {
            get { return _volume; }
            set
            {
                SetProperty(ref _volume, value);
            }
        }

        [JsonIgnore, XmlIgnore]
        public Guid ModelWeightId { get; set; }

        [JsonProperty(nameof(Weight))]
        ModelWeight _weight = new(-1, Enums.Unit.g);
        [JsonIgnore]
        [ManyToOne(nameof(ModelWeightId))]
        public ModelWeight Weight
        {
            get { return _weight; }
            set
            {
                SetProperty(ref _weight, value);
            }
        }

        [JsonProperty(nameof(PrintTime))]
        double _printTime = 0;
        [JsonIgnore]
        public double PrintTime
        {
            get { return _printTime; }
            set
            {
                SetProperty(ref _printTime, value);
            }
        }

        [JsonProperty(nameof(Quantity))]
        int _quantity = 1;
        [JsonIgnore]
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                SetProperty(ref _quantity, value);
            }
        }

        [JsonProperty(nameof(MultiplyPrintTimeWithQuantity))]
        bool _multiplyPrintTimeWithQuantity = true;
        [JsonIgnore]
        public bool MultiplyPrintTimeWithQuantity
        {
            get { return _multiplyPrintTimeWithQuantity; }
            set
            {
                SetProperty(ref _multiplyPrintTimeWithQuantity, value);
            }
        }

        [JsonProperty(nameof(PrintTimeQuantityFactor))]
        double _printTimeQuantityFactor = 1;
        [JsonIgnore]
        public double PrintTimeQuantityFactor
        {
            get { return _printTimeQuantityFactor; }
            set
            {
                SetProperty(ref _printTimeQuantityFactor, value);
            }
        }
        #endregion

        #region Constructor
        public File3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return this.Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is not File3d item)
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
