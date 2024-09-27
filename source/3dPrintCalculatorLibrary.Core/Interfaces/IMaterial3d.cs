using AndreasReitberger.Print3d.Core.Enums;

#if SQL
using AndreasReitberger.Print3d.SQLite.MaterialAdditions;

namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IMaterial3d : ICloneable
    {
        #region Properties
        public Guid Id { get; set; }
#if SQL
        public Guid CalculationId { get; set; }
        public Guid CalculationProfileId { get; set; }
#endif
        public string Name { get; set; }
        public string SKU { get; set; }
        public Unit Unit { get; set; }
        public double PackageSize { get; set; }
        public double Density { get; set; }
        public double FactorLToKg { get; set; }
#if SQL
        public List<Material3dAttribute> Attributes { get; set; }
        public List<Material3dProcedureAttribute> ProcedureAttributes { get; set; }
        public List<Material3dColor> Colors { get; set; }
        public Material3dType? TypeOfMaterial { get; set; }
        public Manufacturer? Manufacturer { get; set; }
#else
        public IList<IMaterial3dAttribute> Attributes { get; set; }
        public IList<IMaterial3dProcedureAttribute> ProcedureAttributes { get; set; }
        public IList<IMaterial3dColor> Colors { get; set; }
        public IMaterial3dType? TypeOfMaterial { get; set; }
        public IManufacturer? Manufacturer { get; set; }
        public Material3dFamily MaterialFamily { get; set; }
#endif
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
