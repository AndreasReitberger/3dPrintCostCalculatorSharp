using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.PrinterAdditions
{
    public partial class Printer3dSlicerConfig : RealmObject, IPrinter3dSlicerConfig
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public double AMax_xy { get; set; } = 1000;

        public double AMax_z { get; set; } = 1000;

        public double AMax_e { get; set; } = 5000;

        public double AMax_eExtrude { get; set; } = 1250;

        public double AMax_eRetract { get; set; } = 1250;

        public double PrintDurationCorrection { get; set; } = 1;

        public string PrinterName { get; set; } = string.Empty;

        #endregion

        #region Constructor
        public Printer3dSlicerConfig()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
