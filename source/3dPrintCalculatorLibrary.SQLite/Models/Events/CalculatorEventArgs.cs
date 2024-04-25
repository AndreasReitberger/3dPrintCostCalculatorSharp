using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Print3d.SQLite.Events
{
    public class CalculatorEventArgs : EventArgs
    {
        #region Properties
        public string Message { get; set; } = string.Empty;
        public Guid CalculationId { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
