using Newtonsoft.Json;


namespace AndreasReitberger.Print3d.SQLite.Events
{
    public class MaterialsChangedDatabaseEventArgs : DatabaseEventArgs
    {
        #region Properties
        public List<Material3d> Materials { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}
