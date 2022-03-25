using AndreasReitberger.Enums;

namespace AndreasReitberger.Models.CalculationAdditions
{
    public class ProcedureSpecificAddition
    {
        #region Properties
        public Printer3dType Procedure { get; set; } = Printer3dType.FDM;
        public ProcedureSpecificCalculationType CalculationType { get; set; } = ProcedureSpecificCalculationType.PerPart;
        public double Addition { get; set; }
        public bool IsPercantageAddition { get; set; }
        #endregion
    }
}
