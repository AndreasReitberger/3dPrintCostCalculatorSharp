using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.SlicerAdditions
{
    public partial class Slicer3dCommand : ObservableObject, ISlicer3dCommand
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid SlicerId { get; set; }

        [ObservableProperty]
        public partial Slicer3d? Slicer { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Command { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string OutputFilePatternString { get; set; } = string.Empty;

        [ObservableProperty]
        public partial bool AutoAddFilePath { get; set; }

        #endregion

        #region Constructor
        public Slicer3dCommand()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
