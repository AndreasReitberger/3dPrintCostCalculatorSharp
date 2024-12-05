
using AndreasReitberger.Print3d.Core.Enums;
#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IWorkstepUsageParameter
    {
        #region Properties
        public Guid Id { get; set; }
        public WorkstepUsageParameterType ParameterType { get; set; }
        public double Value { get; set; }
        #endregion
    }
}
