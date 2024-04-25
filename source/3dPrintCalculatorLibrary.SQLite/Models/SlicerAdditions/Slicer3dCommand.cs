using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.SQLite.SlicerAdditions
{
    [Table("SlicerCommands")]
    public partial class Slicer3dCommand : ObservableObject, ISlicer3dCommand
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        Guid slicerId;

        [ObservableProperty]
        [property: ManyToOne(nameof(SlicerId), CascadeOperations = CascadeOperation.All)]
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
