namespace AndreasReitberger.Print3d.SQLite.Interfaces
{
    public interface IWorkstepUsageCalculation3d
    {
        #region Properties
        public Guid WorkstepUsageId { get; set; }

        public Guid CalculationId { get; set; }
        #endregion
    }
}
