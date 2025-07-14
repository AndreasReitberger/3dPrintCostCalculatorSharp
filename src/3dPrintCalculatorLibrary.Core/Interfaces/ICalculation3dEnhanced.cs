using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Events;

#if SQL
using AndreasReitberger.Print3d.SQLite.CalculationAdditions;
using System.Collections.ObjectModel;

namespace AndreasReitberger.Print3d.SQLite.Interfaces
{
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
{
#endif
    public interface ICalculation3dEnhanced : ICalculation3d
    {
        #region Properties

        #region Details
#if SQL
        public List<Printer3d?> AvailablePrinters { get; }
        public List<Material3d?> AvailableMaterials { get; }
        public List<Print3dInfo> PrintInfos { get; set; }
#else
        public IList<IPrinter3d?> AvailablePrinters { get; }
        public IList<IMaterial3d?> AvailableMaterials { get; }
        public IList<IPrint3dInfo> PrintInfos { get; set; }
#endif
        #endregion

        #endregion
    }
}
