using AndreasReitberger.Print3d.Enums;
using System;

namespace AndreasReitberger.Print3d.Interface
{
    public interface ISlicer3d
    {
        #region Properties
        public Guid Id { get; set; }
        public Slicer SlicerName { get; set; }
        public SlicerExecutionType ExecutionType { get; set; }
        public string InstallationPath { get; set; }
        public string DownloadUri { get; set; }
        public string Author { get; set; }
        public string RepoUri { get; set; }
        public Version Version { get; set; }
        public Version LatestVersion { get; set; }
        #endregion
    }
}
