#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IManufacturer
    {
        #region Properties 
        public Guid Id { get; set; }
#if SQL
        public Guid SupplierId { get; set; }
#endif
        public string Name { get; set; }
        public string DebitorNumber { get; set; }
        public bool IsActive { get; set; }
        public string Website { get; set; }
        public string Note { get; set; }
        public string CountryCode { get; set; }
        #endregion

    }
}
