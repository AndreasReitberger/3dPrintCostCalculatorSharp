using AndreasReitberger.Enums;
using AndreasReitberger.Models.MaterialAdditions;
//using SQLite;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Models
{
    public class Material3d : ICloneable
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties
        //[PrimaryKey]
        public Guid Id
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
        public List<Material3dAttribute> Attributes
        { get; set; } = new List<Material3dAttribute>();
        public List<Material3dProcedureAttribute> ProcedureAttributes
        { get; set; } = new List<Material3dProcedureAttribute>();
        public Material3dFamily MaterialFamily
        { get; set; } = Material3dFamily.Filament;
        public Material3dType TypeOfMaterial
        { get; set; }
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
        #endregion

        #region Constructor
        public Material3d() { }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return this.Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is not Material3d item)
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
