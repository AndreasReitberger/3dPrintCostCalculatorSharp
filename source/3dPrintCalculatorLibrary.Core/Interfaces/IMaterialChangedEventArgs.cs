#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{ 
    public interface IMaterialChangedEventArgs : ICalculatorEventArgs
    {
        public IMaterial3d? Material { get; set; }
    }
}
