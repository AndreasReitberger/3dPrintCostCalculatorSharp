using AndreasReitberger.Print3d.Core.Enums;

#if SQL
using AndreasReitberger.Print3d.SQLite.CalculationAdditions;

namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface ICalculationProcedureAttribute
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid CalculationId { get; set; }
        public Material3dFamily Family { get; set; }
        public ProcedureAttribute Attribute { get; set; }
#if SQL
        public List<CalculationProcedureParameter> Parameters { get; set; }
#else
        public IList<ICalculationProcedureParameter> Parameters { get; set; }
#endif
        public CalculationLevel Level { get; set; }
        public bool PerPiece { get; set; }
        public bool PerFile { get; set; }
    #endregion
    }
}
