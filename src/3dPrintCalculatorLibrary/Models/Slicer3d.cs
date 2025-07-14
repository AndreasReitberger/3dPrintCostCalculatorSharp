using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models
{
    public partial class Slicer3d : ObservableObject, ISlicer3d
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DisplayName))]
        public partial Slicer SlicerName { get; set; } = Slicer.Unkown;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DisplayName))]
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
