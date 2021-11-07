using AndreasReitberger.Enums;
using System.Collections.Generic;

namespace AndreasReitberger.Utilities
{
    public class UnitFactor
    {
        public static Dictionary<Unit, int> UnitFactors = new()
        {
            { Unit.g, 1 },
            { Unit.kg, 1000 },
            { Unit.ml, 1 },
            { Unit.l, 1000 },
        };
        public static int GetUnitFactor(Unit unit)
        {
            if (UnitFactors.ContainsKey(unit))
                return UnitFactors[unit];
            else return 1;
        }
    }
}
