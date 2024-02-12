using AndreasReitberger.Print3d.Core.Interfaces;
using Newtonsoft.Json;

namespace AndreasReitberger.Print3d.Core.Events
{
    public class MaterialChangedEventArgs : CalculatorEventArgs
    {
        #region Properties
        public IMaterial3d? Material { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}
