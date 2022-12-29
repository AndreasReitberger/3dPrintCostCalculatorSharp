using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Models.Events
{
    public class WorkstepsChangedDatabaseEventArgs : DatabaseEventArgs
    {
        #region Properties
        public List<Workstep> Worksteps { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
