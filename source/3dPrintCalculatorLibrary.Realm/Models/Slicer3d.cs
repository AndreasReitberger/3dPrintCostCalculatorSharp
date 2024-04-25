using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm
{
    public partial class Slicer3d : RealmObject, ISlicer3d
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Slicer SlicerName
        {
            get => (Slicer)SlicerNameId;
            set { SlicerNameId = (int)value; }
        }
        public int SlicerNameId { get; set; } = (int)Slicer.Unkown;

        public SlicerExecutionType ExecutionType
        {
            get => (SlicerExecutionType)ExecutionTypeId;
            set { ExecutionTypeId = (int)value; }
        }
        public int ExecutionTypeId { get; set; } = (int)SlicerExecutionType.GUI;

        public string InstallationPath { get; set; } = string.Empty;

        public string DownloadUri { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string RepoUri { get; set; } = string.Empty;

        public Version Version
        {
            get => Version.Parse(VersionString);
            set => VersionString = value.ToString();
        }
        public string VersionString { get; set; } = string.Empty;

        public Version LatestVersion
        {
            get => Version.Parse(LatestVersionString);
            set => LatestVersionString = value.ToString();
        }
        public string LatestVersionString { get; set; } = string.Empty;
        #endregion

        #region Constructor 
        public Slicer3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
