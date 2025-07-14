#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IFile3d : ICloneable
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public object? File { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public double Volume { get; set; }
#if SQL
        public Guid WeightId { get; set; }
        public File3dWeight? Weight { get; set; }
#else
        public IFile3dWeight? Weight { get; set; }
#endif
        public double PrintTime { get; set; }
        public byte[] Image { get; set; }

        #region Legacy
        [Obsolete("Only for legacy support of the `Calculation3d` class")]
        public int Quantity { get; set; }

        [Obsolete("Only for legacy support of the `Calculation3d` class")]
        public bool MultiplyPrintTimeWithQuantity { get; set; }

        [Obsolete("Only for legacy support of the `Calculation3d` class")]
        public double PrintTimeQuantityFactor { get; set; }
        #endregion

        #endregion

    }
}
