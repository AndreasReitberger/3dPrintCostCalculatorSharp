using Newtonsoft.Json;

namespace AndreasReitberger.Print3d.SQLite.Events
{
    public class HourlyMachineRatesChangedDatabaseEventArgs : DatabaseEventArgs
    {
        #region Properties
        public List<HourlyMachineRate> HourlyMachineRates { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}
