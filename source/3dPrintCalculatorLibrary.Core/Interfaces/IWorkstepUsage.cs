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
        public IWorkstep? Workstep { get; set; }
        public IWorkstepUsageParameter? UsageParameter { get; set; }
        public double TotalCosts { get; set; }
        #endregion

        #region Methods

        public double GetTotalCosts();

        #endregion
    }
}
