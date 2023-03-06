using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Realm.CalculationAdditions
{
    public static class CalculationProcedureParameterExtension
    {
        #region Methods
        public static void AddRange(this CalculationProcedureParameter parameter, List<CalculationProcedureParameterAddition> items)
        {
            items?.ForEach(item => parameter.Additions.Add(item));
        }
        #endregion
    }
}
