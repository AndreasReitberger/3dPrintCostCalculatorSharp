using System;

namespace AndreasReitberger.Print3d.Interface
{
    public interface ICalculationProcedureParameterAddition
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid CalculationProcedureParameterId{ get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        #endregion
    }
}
