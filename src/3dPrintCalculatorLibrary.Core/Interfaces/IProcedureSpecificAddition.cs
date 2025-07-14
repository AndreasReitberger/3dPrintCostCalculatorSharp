using AndreasReitberger.Print3d.Core.Enums;

#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IProcedureSpecificAddition
    {
        #region Properties
        public Guid Id { get; set; }
        public Printer3dType Procedure { get; set; }
        public ProcedureSpecificCalculationType CalculationType { get; set; }
        public double Addition { get; set; }
        public bool IsPercantageAddition { get; set; }
        #endregion
    }
}
