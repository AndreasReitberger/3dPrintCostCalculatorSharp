using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.SQLite.Events
{
    public class FilesChangedDatabaseEventArgs : DatabaseEventArgs
    {
        #region Properties
        public List<File3d> Files { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
