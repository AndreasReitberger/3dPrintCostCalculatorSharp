using AndreasReitberger.Print3d.Enums;
using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IMaterial3d
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid CalculationId { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public Unit Unit { get; set; }
        public double PackageSize { get; set; }
        public double Density { get; set; }
        public double FactorLToKg { get; set; }
        public Material3dFamily MaterialFamily { get; set; }
        public Guid MaterialTypeId { get; set; }
        //public IMaterial3dType TypeOfMaterial { get; set; }
        public Guid ManufacturerId { get; set; }
        //public IManufacturer Manufacturer { get; set; }
        public double UnitPrice { get; set; }
        public double Tax { get; set; }
        public bool PriceIncludesTax { get; set; }
        public string Uri { get; set; }
        public string ColorCode { get; set; }
        public string Note { get; set; }
        public string SafetyDatasheet { get; set; }
        public string TechnicalDatasheet { get; set; }
        public Unit SpoolWeightUnit { get; set; }
        public double SpoolWeight { get; set; }
        #endregion

        #region Lists
        /*
        public List<IMaterial3dAttribute> Attributes { get; set; }
        public List<IMaterial3dProcedureAttribute> ProcedureAttributes { get; set; }
        public List<IMaterial3dColor> Colors { get; set; }
        */
        #endregion

    }
}
