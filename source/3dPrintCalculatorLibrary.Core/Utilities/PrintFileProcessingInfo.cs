﻿using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Core.Utilities
{
    public partial class PrintFileProcessingInfo : ObservableObject, IPrintFileProcessingInfo
    {
        #region Properties

        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        Guid fileId;

        [ObservableProperty]
        string fileName = string.Empty;

        [ObservableProperty]
        string filePath = string.Empty;

        [ObservableProperty]
        double progress = 0;
        partial void OnProgressChanged(double value)
        {
            IsDone = value >= 100;
        }

        [ObservableProperty]
        bool isDone = false;

        [ObservableProperty]
        double printDuration = 0;

        [ObservableProperty]
        double volume = 0;

        #endregion

        #region Ctor
        public PrintFileProcessingInfo()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods

        public File3d ToFile()
        {
            return new File3d()
            {
                Name = FileName,
                FilePath = FilePath,
                PrintTime = PrintDuration,
                Volume = Volume,
            };
        }

        #endregion
    }
}