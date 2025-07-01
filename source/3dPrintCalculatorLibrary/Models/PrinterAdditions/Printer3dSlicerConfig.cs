using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.PrinterAdditions
{
    public partial class Printer3dSlicerConfig : ObservableObject, IPrinter3dSlicerConfig
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string PrinterName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial double AMax_xy { get; set; } = 1000;

        [ObservableProperty]
        public partial double AMax_z { get; set; } = 1000;

        [ObservableProperty]
        public partial double AMax_e { get; set; } = 5000;

        [ObservableProperty]
        public partial double AMax_eExtrude { get; set; } = 1250;

        [ObservableProperty]
        public partial double AMax_eRetract { get; set; } = 1250;

        [ObservableProperty]
        public partial double PrintDurationCorrection { get; set; } = 1;

        #endregion

        #region Constructor
        public Printer3dSlicerConfig()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
