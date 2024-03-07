using AndreasReitberger.Print3d.Enums;
using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IWorkstep
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid CalculationId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
        public CalculationType CalculationType { get; set; }
        public WorkstepType Type { get; set; }
        public string Note { get; set; }
        #endregion

    }
}
