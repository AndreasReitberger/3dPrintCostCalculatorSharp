using Newtonsoft.Json;

namespace AndreasReitberger.Print3d.Core.Models.Events
{
    public class CalculatorEventArgs : EventArgs
    {
        #region Properties
        public string? Message { get; set; }
        public Guid CalculationId { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}
