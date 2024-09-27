#if SQL
using AndreasReitberger.Print3d.SQLite.FileAdditions;

namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IFile3d : ICloneable
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public object File { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public double Volume { get; set; }
#if SQL
        public File3dWeight? Weight { get; set; }
#else
        public IFile3dWeight? Weight { get; set; }
#endif
        public double PrintTime { get; set; }
        public byte[] Image { get; set; }
        #endregion

    }
}
