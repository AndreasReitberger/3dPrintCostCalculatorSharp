using System;

namespace AndreasReitberger.Print3d.Interface
{
    public interface IWorkstepDuration
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid CalculationId { get; set; }
        //public ICalculation3d Calculation { get; set; }
        public Guid WorkstepId { get; set; }
        public double Duration { get; set; }
        #endregion
    }
}
