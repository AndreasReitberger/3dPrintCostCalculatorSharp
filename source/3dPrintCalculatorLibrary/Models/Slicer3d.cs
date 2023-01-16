using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Print3d.Models
{
    public partial class Slicer3d : ObservableObject, ISlicer3d
    {
        #region Properties
        [ObservableProperty]
        public Guid id;

        [ObservableProperty]
        public Slicer slicerName = Slicer.Unkown;

        [ObservableProperty]
        public SlicerExecutionType executionType = SlicerExecutionType.GUI;

        [ObservableProperty]
        public string installationPath;

        [ObservableProperty]
        public string downloadUri;

        [ObservableProperty]
        public string author;

        [ObservableProperty]
        public string repoUri;

        [ObservableProperty]
        public Version version;

        [ObservableProperty]
        public Version latestVersion;
        #endregion

        #region Constructor 
        public Slicer3d() 
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
