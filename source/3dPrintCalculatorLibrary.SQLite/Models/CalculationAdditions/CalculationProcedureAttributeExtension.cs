namespace AndreasReitberger.Print3d.SQLite.CalculationAdditions
{
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
