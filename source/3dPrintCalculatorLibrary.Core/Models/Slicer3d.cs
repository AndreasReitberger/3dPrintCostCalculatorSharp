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
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

        [ObservableProperty]
        Slicer slicerName = Slicer.Unkown;

        [ObservableProperty]
        SlicerExecutionType executionType = SlicerExecutionType.GUI;

        [ObservableProperty]
        string installationPath = string.Empty;

        [ObservableProperty]
        string downloadUri = string.Empty;

        [ObservableProperty]
        string author = string.Empty;

        [ObservableProperty]
        string repoUri = string.Empty;

        [ObservableProperty]
        Version? version;

        [ObservableProperty]
        Version? latestVersion;

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
