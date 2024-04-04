using Newtonsoft.Json;

namespace AndreasReitberger.Print3d.SQLite.Events
{
    public class CalculationChangedDatabaseEventArgs : DatabaseEventArgs
    {
        #region Properties
        public List<Calculation3d> Calculations { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
