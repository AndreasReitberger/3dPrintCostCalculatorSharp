using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table("Slicers")]
    public partial class Slicer3d : ObservableObject, ISlicer3d
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
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
