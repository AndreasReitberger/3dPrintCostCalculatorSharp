using AndreasReitberger.Print3d.Enums;

namespace AndreasReitberger.Print3d.Interface
{
    public interface IProcedureSpecificAddition
    {
        #region Properties
        public Printer3dType Procedure { get; set; }
        public ProcedureSpecificCalculationType CalculationType { get; set; }
        public double Addition { get; set; }
        public bool IsPercantageAddition { get; set; }
        #endregion
    }
}
