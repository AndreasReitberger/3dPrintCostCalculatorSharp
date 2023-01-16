using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System;

namespace AndreasReitberger.Print3d.SQLite.PrinterAdditions
{
    [Table("SlicerConfigurations")]
    public partial class Printer3dSlicerConfig : ObservableObject, IPrinter3dSlicerConfig
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        public Guid id;

        [ObservableProperty]
        public double aMax_xy = 1000;

        [ObservableProperty]
        public double aMax_z = 1000;

        [ObservableProperty]
        public double aMax_e = 5000;

        [ObservableProperty]
        public double aMax_eExtrude = 1250;

        [ObservableProperty]
        public double aMax_eRetract = 1250;

        [ObservableProperty]
        public double printDurationCorrection = 1;

        #endregion

        #region Constructor
        public Printer3dSlicerConfig()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
