using System;
using System.Collections.Generic;
using System.Text;

namespace AndreasReitberger.Models.PrinterAdditions
{
    public class Printer3dSlicerConfig
    {
        #region Properties

        public double AMax_xy { get; set; } = 1000;
        public double AMax_z { get; set; } = 1000;
        public double AMax_e { get; set; } = 5000;
        public double AMax_eExtrude { get; set; } = 1250;
        public double AMax_eRetract { get; set; } = 1250;
        public double PrintDurationCorrection { get; set; } = 1;

        #endregion
    }
}
