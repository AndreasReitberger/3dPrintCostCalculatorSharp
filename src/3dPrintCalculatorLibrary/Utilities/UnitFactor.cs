using AndreasReitberger.Print3d.Enums;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Utilities
{
    public class UnitFactor
    {
        public static Dictionary<Unit, int> UnitFactors = new()
        {
            { Unit.Gram, 1 },
            { Unit.Kilogram, 1000 },
            { Unit.MetricTons, 1000 * 1000 },
            { Unit.Mililiters, 1 },
            { Unit.Liters, 1000 },
        };
        public static int GetUnitFactor(Unit unit)
        {
            if (UnitFactors.ContainsKey(unit))
                return UnitFactors[unit];
            else return 1;
        }
    }
}
