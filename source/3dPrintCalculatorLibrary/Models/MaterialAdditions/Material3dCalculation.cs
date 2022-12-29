using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.Models.MaterialAdditions
{
    public class Material3dCalculation
    {
        [ForeignKey(typeof(Material3d))]
        public Guid MaterialId { get; set; }

        [ForeignKey(typeof(Calculation3d))]
        public Guid CalculationId { get; set; }
    }
}
