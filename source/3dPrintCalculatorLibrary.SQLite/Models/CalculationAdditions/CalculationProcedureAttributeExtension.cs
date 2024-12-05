﻿namespace AndreasReitberger.Print3d.SQLite.CalculationAdditions
{
    [Obsolete("Use from Core namespace")]
    public static class CalculationProcedureAttributeExtension
    {
        #region Methods
        public static void AddRange(this CalculationProcedureAttribute parameter, List<CalculationProcedureParameter> items)
        {
            items?.ForEach(item => parameter.Parameters.Add(item));
        }
        #endregion
    }
}
