using AndreasReitberger.Enums;
using System.Collections.Generic;

namespace AndreasReitberger.Models.CalculationAdditions
{
    public class CalculationProcedureAttribute
    {
        #region Properties
        public Material3dFamily Family { get; set; }
        //public Printer3dType Procedure { get; set; }
        public ProcedureAttribute Attribute { get; set; }
        public List<CalculationProcedureParameter> Parameters { get; set; } = new List<CalculationProcedureParameter>();
        //public Type ValueType { get; set; }
        public CalculationLevel Level { get; set; } = CalculationLevel.Printer;
        #endregion
    }
}
