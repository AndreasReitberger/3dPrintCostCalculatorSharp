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
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        //public IWorkstepCategory Category { get; set; }
        public CalculationType CalculationType { get; set; }
        public double Duration { get; set; }
        public WorkstepType Type { get; set; }
        public double TotalCosts { get; set; }
        public string Note { get; set; }
        #endregion

    }
}
