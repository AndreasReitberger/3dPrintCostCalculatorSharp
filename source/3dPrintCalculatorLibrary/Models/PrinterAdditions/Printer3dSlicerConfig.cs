using SQLite;
using System;

namespace AndreasReitberger.Models.PrinterAdditions
{
    [Table("SlicerConfigurations")]
    public class Printer3dSlicerConfig
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

        #endregion

        #region Constructor
        public Printer3dSlicerConfig()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
