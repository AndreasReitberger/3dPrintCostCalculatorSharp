using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.SQLite.WorkstepAdditions
{
    [Obsolete("Use WorkstepUsageCalculation3dEnhanced instead")]
    public class WorkstepUsageCalculation
    {
        [ForeignKey(typeof(WorkstepUsage))]
        public Guid WorkstepUsageId { get; set; }

        [ForeignKey(typeof(Calculation3d))]
        public Guid CalculationId { get; set; }
    }
}
