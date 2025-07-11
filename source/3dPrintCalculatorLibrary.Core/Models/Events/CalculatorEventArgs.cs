using Newtonsoft.Json;

#if SQL
using AndreasReitberger.Print3d.Core.Interfaces;

namespace AndreasReitberger.Print3d.SQLite.Events
#else
namespace AndreasReitberger.Print3d.Core.Events
#endif
{
    public class CalculatorEventArgs : EventArgs, ICalculatorEventArgs
    {
        #region Properties
        public string? Message { get; set; }
        public Guid CalculationId { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}
