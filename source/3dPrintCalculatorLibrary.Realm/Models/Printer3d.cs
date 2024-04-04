using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Realm.PrinterAdditions;
using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Realm
{
    public partial class Printer3d : RealmObject, IPrinter3d, ICloneable
    {

        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid CalculationId { get; set; }
        [Required]
        public string Model { get; set; } = string.Empty;

        public Printer3dType Type
        {
            get => (Printer3dType)TypeId;
            set { TypeId = (int)value; }
        }
        public int TypeId { get; set; } = (int)Printer3dType.FDM;

        public Guid ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public double Price { get; set; } = 0;

        public double Tax { get; set; } = 0;

        public bool PriceIncludesTax { get; set; } = true;

        public string Uri { get; set; } = string.Empty;

        public Material3dFamily MaterialType
        {
            get => (Material3dFamily)MaterialTypeId;
            set { MaterialTypeId = (int)value; }
        }
        public int MaterialTypeId { get; set; } = (int)Material3dFamily.Filament;

        public double PowerConsumption { get; set; } = 0;

        public double Width { get; set; } = 1;

        public double Depth { get; set; } = 1;

        public double Height { get; set; } = 1;

        public Guid HourlyMachineRateId { get; set; }

        public HourlyMachineRate HourlyMachineRate { get; set; }

        public Guid SlicerConfigId { get; set; }

        public Printer3dSlicerConfig SlicerConfig { get; set; } = new();

        public byte[] Image { get; set; } = Array.Empty<byte>();

        public string Note { get; set; } = string.Empty;

        [JsonIgnore]
        public string Name => !string.IsNullOrEmpty(Manufacturer?.Name) ? $"{Manufacturer.Name}, {Model}" : Model;

        [JsonIgnore]
        public double Volume => CalculateVolume();
        #endregion

        #region Collections
        public IList<Printer3dAttribute> Attributes { get; }

        public IList<Maintenance3d> Maintenances { get; }
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
