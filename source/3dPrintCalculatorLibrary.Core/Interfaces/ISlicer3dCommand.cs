#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface ISlicer3dCommand
    {
        #region Properties
        public Guid Id { get; set; }
#if SQL
        public Guid SlicerId { get; set; }
        public Slicer3d? Slicer { get; set; }
#else
        public ISlicer3d? Slicer { get; set; }
#endif
        public string Name { get; set; }
        public string Command { get; set; }
        public string OutputFilePatternString { get; set; }
        public bool AutoAddFilePath { get; set; }

    #endregion
    }
}
