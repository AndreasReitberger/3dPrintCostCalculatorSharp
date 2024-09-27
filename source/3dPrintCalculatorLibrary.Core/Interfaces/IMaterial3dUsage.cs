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
        public IMaterial3d? Material { get; set; }
        public double PercentageValue { get; set; }
        public double Percentage { get; }
        #endregion 
    }
}
