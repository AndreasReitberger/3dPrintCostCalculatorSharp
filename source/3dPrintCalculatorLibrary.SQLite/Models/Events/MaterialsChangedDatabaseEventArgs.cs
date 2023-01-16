using Newtonsoft.Json;
using System.Collections.Generic;


namespace AndreasReitberger.Print3d.SQLite.Events
{
    public class MaterialsChangedDatabaseEventArgs : DatabaseEventArgs
    {
        #region Properties
        public List<Material3d> Materials { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
