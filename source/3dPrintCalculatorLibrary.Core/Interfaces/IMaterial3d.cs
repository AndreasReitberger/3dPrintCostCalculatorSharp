using AndreasReitberger.Print3d.Core.Enums;

namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IMaterial3d : ICloneable
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public Unit Unit { get; set; }
        public double PackageSize { get; set; }
        public double Density { get; set; }
        public double FactorLToKg { get; set; }
        public IList<IMaterial3dAttribute> Attributes { get; set; }
        public IList<IMaterial3dProcedureAttribute> ProcedureAttributes { get; set; }
        public IList<IMaterial3dColor> Colors { get; set; }
        public Material3dFamily MaterialFamily { get; set; }
        public IMaterial3dType? TypeOfMaterial { get; set; }
        public IManufacturer? Manufacturer { get; set; }
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
        public byte[] Image { get; set; }
        #endregion
    }
}
