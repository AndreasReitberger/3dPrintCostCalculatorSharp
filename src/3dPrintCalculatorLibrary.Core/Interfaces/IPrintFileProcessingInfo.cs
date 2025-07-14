using AndreasReitberger.Print3d.Core;

#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IPrintFileProcessingInfo
    {
        #region Properties

        public Guid Id { get; set; }

        public Guid FileId { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public double Progress { get; set; }

        public bool IsDone { get; set; }

        public double PrintDuration { get; set; }

        public double Volume { get; set; }

        #endregion

        #region Methods

        public File3d ToFile();

        #endregion
    }
}
