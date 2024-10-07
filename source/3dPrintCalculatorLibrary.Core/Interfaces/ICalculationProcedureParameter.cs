using AndreasReitberger.Print3d.Core.Enums;

#if SQL
using AndreasReitberger.Print3d.SQLite.CalculationAdditions;

namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface ICalculationProcedureParameter
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid CalculationProcedureAttributeId { get; set; }
        public ProcedureParameter Type { get; set; }
        public double Value { get; set; }
#if SQL
        public List<CalculationProcedureParameterAddition> Additions { get; set; }
#else
        public IList<ICalculationProcedureParameterAddition> Additions { get; set; }
#endif

        #endregion
    }
}
