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
        Guid id;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DisplayName))]
        Slicer slicerName = Slicer.Unkown;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DisplayName))]
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
