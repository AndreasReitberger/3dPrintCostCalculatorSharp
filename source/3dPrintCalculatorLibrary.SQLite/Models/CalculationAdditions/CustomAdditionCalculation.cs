using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.SQLite.CalculationAdditions
{
    [Obsolete("Use CustomAdditionCalculation3dEnhanced instead")]
    public class CustomAdditionCalculation
    {
        [ForeignKey(typeof(CustomAddition))]
        public Guid CustomAdditionId { get; set; }

        [ForeignKey(typeof(Calculation3d))]
        public Guid CalculationId { get; set; }
    }
}
