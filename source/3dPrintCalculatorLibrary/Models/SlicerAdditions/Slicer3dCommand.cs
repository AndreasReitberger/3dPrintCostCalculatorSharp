using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Xml.Serialization;

namespace AndreasReitberger.Models.SlicerAdditions
{
    [Table("SlicerCommands")]
    public class Slicer3dCommand
    {
        #region Properties
        [PrimaryKey]
        public Guid Id
        { get; set; }

        [JsonIgnore, XmlIgnore]
        public Guid SlicerId 
        { get; set; }

        [ManyToOne(nameof(SlicerId))]
        public Slicer3d Slicer 
        { get; set; }

        public string Name 
        { get; set; }       

        public string Command 
        { get; set; }

        public string OutputFilePatternString 
        { get; set; }

        public bool AutoAddFilePath 
        { get; set; }

        #endregion

        #region Constructor
        public Slicer3dCommand()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
