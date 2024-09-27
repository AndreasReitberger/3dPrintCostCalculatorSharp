#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IPrinter3dSlicerConfig
    {
        #region Properties
        public Guid Id { get; set; }
        public string PrinterName { get; set; }
        public double AMax_xy { get; set; }
        public double AMax_z { get; set; }
        public double AMax_e { get; set; }
        public double AMax_eExtrude { get; set; }
        public double AMax_eRetract { get; set; }
        public double PrintDurationCorrection { get; set; }

        #endregion
    }
}
