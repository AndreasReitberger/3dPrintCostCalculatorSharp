using AndreasReitberger.Print3d.Enums;
using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface ICalculationProcedureParameter
    {
        #region Properties
        public Guid Id { get; set; }

        public Guid CalculationProcedureAttributeId { get; set; }
        public ProcedureParameter Type { get; set; }
        public double Value { get; set; }

        #endregion

        #region List
        //public List<ICalculationProcedureParameterAddition> Additions { get; set; }
        #endregion
    }
}
