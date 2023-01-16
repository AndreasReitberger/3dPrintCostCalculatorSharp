using AndreasReitberger.Print3d.Enums;
using System;

namespace AndreasReitberger.Print3d.Interface
{
    public interface ICalculationProcedureAttribute
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid CalculationId { get; set; }
        public Material3dFamily Family { get; set; }
        public ProcedureAttribute Attribute { get; set; }      
        public CalculationLevel Level { get; set; }
        #endregion

        #region List
        //public List<ICalculationProcedureParameter> Parameters { get; set; }
        #endregion
    }
}
