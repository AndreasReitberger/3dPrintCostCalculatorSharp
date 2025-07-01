using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Slicer3d)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Slicer3d : ObservableObject, ISlicer3d
    {
        #region Properties
#if SQL
        [property: PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Slicer SlicerName { get; set; } = Slicer.Unkown;

        [ObservableProperty]
        public partial SlicerExecutionType ExecutionType { get; set; } = SlicerExecutionType.GUI;

        [ObservableProperty]
        public partial string InstallationPath { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string DownloadUri { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Author { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string RepoUri { get; set; } = string.Empty;

        [ObservableProperty]
        public partial Version? Version { get; set; }

        [ObservableProperty]
        public partial Version? LatestVersion { get; set; }

        public string DisplayName => $"{SlicerName} ({ExecutionType})";
        #endregion

        #region Constructor 
        public Slicer3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
