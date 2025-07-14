namespace AndreasReitberger.Print3d.Core
{
    public static class CalculationProcedureParameterExtension
    {
        #region Methods
        public static void AddRange(this CalculationProcedureParameter parameter, List<CalculationProcedureParameterAddition> items)
        {
            items?.ForEach(parameter.Additions.Add);
        }
        public static void AddRange(this ICalculationProcedureParameter parameter, IList<ICalculationProcedureParameterAddition> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                parameter.Additions.Add(items[i]);
            }
        }
        #endregion
    }
}
