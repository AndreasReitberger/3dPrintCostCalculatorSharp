using System;

namespace AndreasReitberger.Print3d.Enums
{
    public enum Unit
    {
        [Obsolete("Use `Gramm`")]
        g,
        [Obsolete("Use `Kilogramm`")]
        kg,
        [Obsolete("Use `Mililiters`")]
        ml,
        [Obsolete("Use `Liters`")]
        l,
        Gramm,
        Kilogramm,
        MetricTons,
        Mililiters,
        Liters,
    }
}
