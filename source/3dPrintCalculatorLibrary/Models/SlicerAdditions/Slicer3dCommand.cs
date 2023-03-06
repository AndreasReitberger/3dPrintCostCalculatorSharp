using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

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
        Slicer3d slicer;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string command;

        [ObservableProperty]
        string outputFilePatternString;

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
