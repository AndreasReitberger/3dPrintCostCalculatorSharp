using Newtonsoft.Json;

namespace AndreasReitberger.Models.Events
{
    public class MaterialChangedEventArgs : CalculatorEventArgs
    {
        #region Properties
        public Material3d Material { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
