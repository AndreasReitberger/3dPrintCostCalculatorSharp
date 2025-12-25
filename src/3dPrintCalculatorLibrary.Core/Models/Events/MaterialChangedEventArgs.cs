#if SQL
using AndreasReitberger.Print3d.Core.Events;

namespace AndreasReitberger.Print3d.SQLite.Events
#else

namespace AndreasReitberger.Print3d.Core.Events
#endif
{
    public class MaterialChangedEventArgs : CalculatorEventArgs, IMaterialChangedEventArgs
    {
        #region Properties
#if SQL
        public Material3d? Material { get; set; }
#else
        public IMaterial3d? Material { get; set; }
#endif
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.MaterialChangedEventArgs);

        #endregion
    }
}
