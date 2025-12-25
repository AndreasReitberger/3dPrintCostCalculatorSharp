namespace AndreasReitberger.Print3d.SQLite.Events
{
    public class HourlyMachineRatesChangedDatabaseEventArgs : DatabaseEventArgs
    {
        #region Properties
        public List<HourlyMachineRate> HourlyMachineRates { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.HourlyMachineRatesChangedDatabaseEventArgs);
        #endregion
    }
}
