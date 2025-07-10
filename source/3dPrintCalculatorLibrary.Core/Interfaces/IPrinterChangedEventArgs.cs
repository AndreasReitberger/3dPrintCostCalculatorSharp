#if SQL
using AndreasReitberger.Print3d.Core.Interfaces;

namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{ 
    public interface IPrinterChangedEventArgs : ICalculatorEventArgs
    {
#if SQL
        public Printer3d? Printer { get; set; }
#else
        public IPrinter3d? Printer { get; set; }
#endif
    }
}
