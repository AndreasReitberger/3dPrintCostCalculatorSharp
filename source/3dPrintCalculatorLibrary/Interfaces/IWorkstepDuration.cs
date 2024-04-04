using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    [Obsolete("Use the IWorkstepUsage class instead")]
    public interface IWorkstepDuration
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid WorkstepId { get; set; }
        public double Duration { get; set; }
        #endregion
    }
}
