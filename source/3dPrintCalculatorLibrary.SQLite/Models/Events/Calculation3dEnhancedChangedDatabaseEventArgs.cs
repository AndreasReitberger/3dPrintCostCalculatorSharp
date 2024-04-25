using Newtonsoft.Json;

namespace AndreasReitberger.Print3d.SQLite.Events
{
    public class Calculation3dEnhancedChangedDatabaseEventArgs : DatabaseEventArgs
    {
        #region Properties
        public List<Calculation3dEnhanced> Calculations { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}
