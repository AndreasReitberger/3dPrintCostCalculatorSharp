using AndreasReitberger.Print3d.Core.Enums;

namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface ICalculationProcedureParameter
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid CalculationProcedureAttributeId { get; set; }
        public ProcedureParameter Type { get; set; }
        public double Value { get; set; }
        public IList<ICalculationProcedureParameterAddition> Additions { get; set; }

        #endregion
    }
}
