#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IWorkstepUsage
    {
        #region Properties
        public Guid Id { get; set; }
#if SQL
        public Guid CalculationId { get; set; }
        public Guid CalculationProfileId { get; set; }
        public Workstep? Workstep { get; set; }
        public WorkstepUsageParameter? UsageParameter { get; set; }
#else
        public IWorkstep? Workstep { get; set; }
        public IWorkstepUsageParameter? UsageParameter { get; set; }
#endif
        public double TotalCosts { get; set; }
        #endregion

        #region Methods

        public double GetTotalCosts();

        #endregion
    }
}
