namespace AndreasReitberger.Print3d.SQLite.Interfaces
{
    internal interface ICustomAdditionCalculation3dEnhanced
    {
        #region Properties
        public Guid CustomAdditionId { get; set; }

        public Guid CalculationId { get; set; }
        #endregion
    }
}
