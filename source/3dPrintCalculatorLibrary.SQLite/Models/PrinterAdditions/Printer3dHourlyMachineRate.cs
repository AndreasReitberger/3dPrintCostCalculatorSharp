using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.SQLite.PrinterAdditions
{
    public class Printer3dHourlyMachineRate
    {
        [ForeignKey(typeof(Material3d))]
        public Guid MaterialId { get; set; }

        [ForeignKey(typeof(Calculation3d))]
        public Guid CalculationId { get; set; }
    }
}
