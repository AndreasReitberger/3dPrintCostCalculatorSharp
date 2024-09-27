#if SQL
using AndreasReitberger.Print3d.SQLite.FileAdditions;

namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IFile3dUsage
    {
        #region Properties
        public Guid Id { get; set; }
#if SQL
        public Guid PrintInfoId { get; set; }
        public Guid FileId { get; set; }
        public File3d? File { get; set; }
#else
        public IFile3d? File { get; set; }
#endif
        public int Quantity { get; set; }
        public bool MultiplyPrintTimeWithQuantity { get; set; }
        public double PrintTimeQuantityFactor { get; set; }
        #endregion
    }
}
