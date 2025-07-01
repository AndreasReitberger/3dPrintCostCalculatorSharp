namespace AndreasReitberger.Print3d.Core
{
    public static class CalculationProcedureAttributeExtension
    {
        #region Methods
        public static void AddRange(this CalculationProcedureAttribute parameter, List<CalculationProcedureParameter> items)
        {
            items?.ForEach(parameter.Parameters.Add);
        }
        #endregion
    }
}
