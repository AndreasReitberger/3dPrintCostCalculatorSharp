#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IMaterial3dUsage
    {
        #region Properties
        public Guid Id { get; set; }
#if SQL
        public Guid PrintInfoId { get; set; }
        public Guid MaterialId { get; set; }
        public Material3d? Material { get; set; }
#else
        public IMaterial3d? Material { get; set; }
#endif
        public double PercentageValue { get; set; }
        public double Percentage { get; }
        #endregion
    }
}
