using AndreasReitberger.Enums;
using AndreasReitberger.Models.MaterialAdditions;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AndreasReitberger.Models
{
    [Table("Materials")]
    public class Material3d : ICloneable
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties
        [PrimaryKey]
        public Guid Id
        { get; set; }

        [ForeignKey(typeof(Calculation3d))]
        public Guid CalculationId
        { get; set; }
        [ManyToOne]
        public Calculation3d Calculation
        { get; set; }
        public string Name
        { get; set; } = string.Empty;
        public string SKU
        { get; set; } = string.Empty;
        public Unit Unit
        { get; set; } = Unit.kg;
        public double PackageSize
        { get; set; } = 1;
        public double Density
        { get; set; } = 1;
        public double FactorLToKg
        { get; set; } = 1;

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Material3dAttribute> Attributes
        { get; set; } = new List<Material3dAttribute>();

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Material3dProcedureAttribute> ProcedureAttributes
        { get; set; } = new List<Material3dProcedureAttribute>();

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Material3dColor> Colors
        { get; set; } = new List<Material3dColor>();

        public Material3dFamily MaterialFamily
        { get; set; } = Material3dFamily.Filament;

        [JsonIgnore, XmlIgnore]
        public Guid MaterialTypeId { get; set; }
        [ManyToOne(nameof(MaterialTypeId))]
        public Material3dType TypeOfMaterial
        { get; set; }

        [JsonIgnore, XmlIgnore]
        public Guid ManufacturerId { get; set; }
        [ManyToOne(nameof(ManufacturerId))]
        public Manufacturer Manufacturer
        { get; set; }
        public double UnitPrice
        { get; set; } = 0;
        public double Tax
        { get; set; } = 0;
        public bool PriceIncludesTax
        { get; set; } = true;
        public string Uri
        { get; set; } = string.Empty;
        public string ColorCode
        { get; set; } = string.Empty;
        public string Note
        { get; set; } = string.Empty;
        public string SafetyDatasheet
        { get; set; } = string.Empty;
        public string TechnicalDatasheet
        { get; set; } = string.Empty;

        public Unit SppolWeightUnit
        { get; set; } = Unit.g;
        public double SppolWeight
        { get; set; } = 200;
        #endregion

        #region Constructor
        public Material3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is not Material3d item)
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
