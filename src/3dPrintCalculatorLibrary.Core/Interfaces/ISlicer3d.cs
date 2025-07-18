﻿using AndreasReitberger.Print3d.Core.Enums;

#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
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
        public Version? Version { get; set; }
        public Version? LatestVersion { get; set; }
        public string DisplayName { get; }
        #endregion
    }
}
