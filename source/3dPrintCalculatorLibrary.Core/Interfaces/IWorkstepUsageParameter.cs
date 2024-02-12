using AndreasReitberger.Print3d.Core.Enums;

namespace AndreasReitberger.Print3d.Core.Interfaces
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
