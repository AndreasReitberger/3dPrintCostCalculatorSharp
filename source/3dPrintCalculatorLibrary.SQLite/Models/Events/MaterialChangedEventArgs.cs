using Newtonsoft.Json;

namespace AndreasReitberger.Print3d.SQLite.Events
{
    [Obsolete("User core variant")]
    public class MaterialChangedEventArgsOld : CalculatorEventArgsOld
    {
        #region Properties
        public Material3d? Material { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
