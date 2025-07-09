#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{ 
    public interface ICalculatorEventArgs
    {
        public string? Message { get; set; }
        public Guid CalculationId { get; set; }
    }
}
