#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IItem3d : ICloneable
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
#if SQL
        public Guid ManufacturerId { get; set; }
        public Manufacturer? Manufacturer { get; set; }
#else
        public IManufacturer? Manufacturer { get; set; }
#endif
        public double PackageSize { get; set; }
        public double PackagePrice { get; set; }
        public double Tax { get; set; }
        public bool PriceIncludesTax { get; set; }
        public string Uri { get; set; }
        public string Note { get; set; }
        public string SafetyDatasheet { get; set; }
        public string TechnicalDatasheet { get; set; }
        public double PricePerPiece { get; }
        #endregion
    }
}
