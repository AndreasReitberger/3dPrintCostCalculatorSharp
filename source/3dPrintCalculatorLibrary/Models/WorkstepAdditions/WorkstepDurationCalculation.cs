using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Models.WorkstepAdditions
{
    public class WorkstepDurationCalculation
    {
        [ForeignKey(typeof(WorkstepDuration))]
        public Guid WorkstepDurationId { get; set; }

        [ForeignKey(typeof(Calculation3d))]
        public Guid CalculationId { get; set; }
    }
}
