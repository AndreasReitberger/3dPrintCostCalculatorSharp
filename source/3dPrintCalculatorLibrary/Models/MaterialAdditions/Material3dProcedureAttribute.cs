using AndreasReitberger.Enums;

namespace AndreasReitberger.Models.MaterialAdditions
{
    public class Material3dProcedureAttribute
    {
        #region Properties
        public Material3dFamily Family { get; set; }
        //public Printer3dType Procedure { get; set; }
        public ProcedureAttribute Attribute { get; set; }
        //public Type ValueType { get; set; }
        public double Value { get; set; }
        #endregion
    }
}
