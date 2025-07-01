using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Realm.MaterialAdditions;
using System;
using System.Collections.Generic;
using Realms;
using Newtonsoft.Json;

namespace AndreasReitberger.Print3d.Realm
{
    public partial class Material3d : RealmObject, IMaterial3d, ICloneable
    {
        #region Clone
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion

        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid CalculationId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        public string SKU { get; set; } = string.Empty;

        public Unit Unit
        {
            get => (Unit)UnitId;
            set { UnitId = (int)value; }
        }
        public int UnitId { get; set; } = (int)Unit.Kilogram;
        public double PackageSize { get; set; } = 1;

        public double Density { get; set; } = 1;

        public double FactorLToKg { get; set; } = 1;

        public Material3dFamily MaterialFamily
        {
            get => (Material3dFamily)MaterialFamilyId;
            set { MaterialFamilyId = (int)value; }
        }
        public int MaterialFamilyId { get; set; } = (int)Material3dFamily.Filament;

        public Guid MaterialTypeId { get; set; }

        public Material3dType? TypeOfMaterial { get; set; }

        public Guid ManufacturerId { get; set; }

        public Manufacturer? Manufacturer { get; set; }

        public double UnitPrice { get; set; } = 0;

        public double Tax { get; set; } = 0;

        public bool PriceIncludesTax { get; set; } = true;

        public string Uri { get; set; } = string.Empty;

        public string ColorCode { get; set; } = string.Empty;

        public string Note { get; set; } = string.Empty;

        public string SafetyDatasheet { get; set; } = string.Empty;

        public string TechnicalDatasheet { get; set; } = string.Empty;

        public Unit SpoolWeightUnit
        {
            get => (Unit)SpoolWeightUnitId;
            set { SpoolWeightUnitId = (int)value; }
        }
        public int SpoolWeightUnitId { get; set; } = (int)Unit.Gram;

        public double SpoolWeight { get; set; } = 200;

        public byte[] Image { get; set; } = [];
        #endregion

        #region Collections

        public IList<Material3dAttribute> Attributes { get; } = [];

        public IList<Material3dProcedureAttribute> ProcedureAttributes { get; } = [];

        public IList<Material3dColor> Colors { get; } = [];
        #endregion

        #region Constructor
        public Material3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not Material3d item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}
