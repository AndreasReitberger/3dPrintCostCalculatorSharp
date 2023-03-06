using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.PrinterAdditions
{
    public partial class Printer3dSlicerConfig : ObservableObject, IPrinter3dSlicerConfig
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        double aMax_xy = 1000;

        [ObservableProperty]
        double aMax_z = 1000;

        [ObservableProperty]
        double aMax_e = 5000;

        [ObservableProperty]
        double aMax_eExtrude = 1250;

        [ObservableProperty]
        double aMax_eRetract = 1250;

        [ObservableProperty]
        double printDurationCorrection = 1;

        #endregion

        #region Constructor
        public Printer3dSlicerConfig()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
