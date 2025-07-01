using Newtonsoft.Json;

namespace AndreasReitberger.Print3d.SQLite.Events
{
    public class PrintersChangedDatabaseEventArgs : DatabaseEventArgs
    {
        #region Properties
        public List<Printer3d> Printers { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}
