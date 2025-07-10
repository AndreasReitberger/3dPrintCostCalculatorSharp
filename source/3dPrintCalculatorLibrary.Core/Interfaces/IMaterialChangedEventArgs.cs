#if SQL
using AndreasReitberger.Print3d.Core.Interfaces;

namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IMaterialChangedEventArgs : ICalculatorEventArgs
    {
#if SQL
        public Material3d? Material { get; set; }
#else
        public IMaterial3d? Material { get; set; }
#endif
    }
}
