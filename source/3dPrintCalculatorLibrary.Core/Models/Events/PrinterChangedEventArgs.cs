using Newtonsoft.Json;

#if SQL
using AndreasReitberger.Print3d.Core.Events;
namespace AndreasReitberger.Print3d.SQLite.Events
#else
namespace AndreasReitberger.Print3d.Core.Events
#endif
{
    public class PrinterChangedEventArgs : CalculatorEventArgs, IPrinterChangedEventArgs
    {
        #region Properties
#if SQL
        public Printer3d? Printer { get; set; }
#else
        public IPrinter3d? Printer { get; set; }
#endif
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}
