using AndreasReitberger.Print3d.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.Models.SlicerAdditions
{
    [Table("SlicerCommands")]
    public partial class Slicer3dCommand : ObservableObject, ISlicer3dCommand
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        public Guid id;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        public Guid slicerId;

        [ObservableProperty]
        [property: ManyToOne(nameof(SlicerId))]
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
