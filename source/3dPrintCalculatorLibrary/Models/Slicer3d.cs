using AndreasReitberger.Print3d.Enums;
using SQLite;
using System;

namespace AndreasReitberger.Print3d.Models
{
    [Table("Slicers")]
    public class Slicer3d
    {
        #region Properties
        [PrimaryKey]
        public Guid Id
        { get; set; }

        public Slicer SlicerName
        { get; set; } = Slicer.Unkown;

        public SlicerExecutionType ExecutionType
        { get; set; } = SlicerExecutionType.GUI;

        public string InstallationPath
        { get; set; }

        public string DownloadUri
        { get; set; }

        public string Author
        { get; set; }

        public string RepoUri
        { get; set; }

        public Version Version
        { get; set; }

        public Version LatestVersion
        { get; set; }
        #endregion

        #region Constructor 
        public Slicer3d() 
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
