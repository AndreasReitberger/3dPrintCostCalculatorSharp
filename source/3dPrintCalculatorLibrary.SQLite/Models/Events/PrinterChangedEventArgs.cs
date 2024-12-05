using Newtonsoft.Json;

namespace AndreasReitberger.Print3d.SQLite.Events
{
    [Obsolete("User core variant")]
    public class PrinterChangedEventArgsOld : CalculatorEventArgsOld
    {
        #region Properties
        public Printer3d? Printer { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
