using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Print3d.Realm.Events
{
    public class CalculatorEventArgs : EventArgs
    {
        #region Properties
        public string Message { get; set; } = string.Empty;
        public Guid CalculationId { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}
