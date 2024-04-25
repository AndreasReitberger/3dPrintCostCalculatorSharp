using Newtonsoft.Json;

namespace AndreasReitberger.Print3d.SQLite.Events
{
    public class WorkstepsChangedDatabaseEventArgs : DatabaseEventArgs
    {
        #region Properties
        public List<Workstep> Worksteps { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
