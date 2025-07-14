namespace AndreasReitberger.Print3d.SQLite.Interfaces
{
    public interface IMaterial3dCalculation3d
    {
        #region Properties
        public Guid MaterialId { get; set; }

        public Guid CalculationId { get; set; }
        #endregion
    }
}
