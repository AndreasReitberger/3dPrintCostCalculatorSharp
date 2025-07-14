using AndreasReitberger.Print3d.Enums;
using System;

namespace AndreasReitberger.Print3d.Interfaces
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
