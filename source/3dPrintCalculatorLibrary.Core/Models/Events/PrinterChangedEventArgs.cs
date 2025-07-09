using Newtonsoft.Json;

namespace AndreasReitberger.Print3d.Core.Events
{
    public class PrinterChangedEventArgs : CalculatorEventArgs, IPrinterChangedEventArgs
    {
        #region Properties
        public IPrinter3d? Printer { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}
