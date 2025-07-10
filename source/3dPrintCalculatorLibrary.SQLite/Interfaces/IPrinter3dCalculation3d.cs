namespace AndreasReitberger.Print3d.SQLite.Interfaces
{
    public interface IPrinter3dCalculation3d
    {
        #region Properties
        public Guid PrinterId { get; set; }

        public Guid CalculationId { get; set; }
        #endregion
    }
}
