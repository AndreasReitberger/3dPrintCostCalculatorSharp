#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface ISupplier
    {
        #region Properties 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DebitorNumber { get; set; }
        public bool IsActive { get; set; }
        public string Website { get; set; }
        public IList<IManufacturer> Manufacturers { get; set; }
        #endregion
    }
}
