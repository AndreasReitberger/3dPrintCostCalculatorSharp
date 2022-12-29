using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.Models.WorkstepAdditions
{
    public class WorkstepCalculation
    {
        [ForeignKey(typeof(Workstep))]
        public Guid WorkstepId { get; set; }

        [ForeignKey(typeof(Calculation3d))]
        public Guid CalculationId { get; set; }
    }
}
