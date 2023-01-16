using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.SQLite.Events
{
    public class CustomersChangedDatabaseEventArgs : DatabaseEventArgs
    {
        #region Properties
        public List<Customer3d> Customers { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
