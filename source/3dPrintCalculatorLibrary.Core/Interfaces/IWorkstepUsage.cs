namespace AndreasReitberger.Print3d.Core.Interfaces
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
