#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{ 
    public interface IPrinterChangedEventArgs : ICalculatorEventArgs
    {
        public IPrinter3d? Printer { get; set; }
    }
}
