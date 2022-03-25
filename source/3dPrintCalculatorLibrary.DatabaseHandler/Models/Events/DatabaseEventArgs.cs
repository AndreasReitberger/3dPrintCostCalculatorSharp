using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Models.Events
{
    public class DatabaseEventArgs : EventArgs
    {
        #region Properties
        public string Message { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
