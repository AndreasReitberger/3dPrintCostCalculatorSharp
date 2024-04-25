using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.SlicerAdditions
{
    public partial class Slicer3dCommand : ObservableObject, ISlicer3dCommand
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        Guid slicerId;

        [ObservableProperty]
        Slicer3d? slicer;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        string command = string.Empty;

        [ObservableProperty]
        string outputFilePatternString = string.Empty;

        [ObservableProperty]
        bool autoAddFilePath;

        #endregion

        #region Constructor
        public Slicer3dCommand()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
