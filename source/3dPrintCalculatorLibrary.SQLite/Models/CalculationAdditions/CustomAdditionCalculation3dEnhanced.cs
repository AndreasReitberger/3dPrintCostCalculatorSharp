using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.SQLite.CalculationAdditions
{
    public class CustomAdditionCalculation3dEnhanced
    {
        [ForeignKey(typeof(CustomAddition))]
        public Guid CustomAdditionId { get; set; }

        [ForeignKey(typeof(Calculation3dEnhanced))]
        public Guid CalculationId { get; set; }
    }
}
