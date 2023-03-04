using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.Realm.SlicerAdditions
{
    public partial class Slicer3dCommand : ObservableObject, ISlicer3dCommand
    {
        #region Properties
        [ObservableProperty]
        public Guid id;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        public Guid slicerId;

        [ObservableProperty]
        public Slicer3d slicer;

        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public string command;

        [ObservableProperty]
        public string outputFilePatternString;

        [ObservableProperty]
        public bool autoAddFilePath;

        #endregion

        #region Constructor
        public Slicer3dCommand()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
