using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IWorkstepUsage
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid WorkstepId { get; set; }
        public double TotalCosts { get; set; }
        #endregion

        #region Methods

        public double GetTotalCosts();

        #endregion
    }
}
