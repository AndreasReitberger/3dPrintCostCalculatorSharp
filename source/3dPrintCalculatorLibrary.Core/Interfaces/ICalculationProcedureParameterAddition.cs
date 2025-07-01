#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface ICalculationProcedureParameterAddition
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid CalculationProcedureParameterId { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        #endregion
    }
}
