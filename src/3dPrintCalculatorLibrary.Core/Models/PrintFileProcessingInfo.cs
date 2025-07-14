using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(PrintFileProcessingInfo)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class PrintFileProcessingInfo : ObservableObject, IPrintFileProcessingInfo
    {
        #region Properties

        [ObservableProperty]
#if SQL
        [PrimaryKey]
#endif
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid FileId { get; set; }

        [ObservableProperty]
        public partial string FileName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string FilePath { get; set; } = string.Empty;

        [ObservableProperty]
        public partial double Progress { get; set; } = 0;
        partial void OnProgressChanged(double value)
        {
            IsDone = value >= 100;
        }

        [ObservableProperty]
        public partial bool IsDone { get; set; } = false;

        [ObservableProperty]
        public partial double PrintDuration { get; set; } = 0;

        [ObservableProperty]
        public partial double Volume { get; set; } = 0;

        #endregion

        #region Ctor
        public PrintFileProcessingInfo()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods

        public File3d ToFile()
        {
            return new File3d()
            {
                Name = FileName,
                FilePath = FilePath,
                PrintTime = PrintDuration,
                Volume = Volume,
            };
        }

        #endregion
    }
}
