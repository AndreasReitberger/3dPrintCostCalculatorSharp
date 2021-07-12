using AndreasReitberger.Enums;
using AndreasReitberger.Utilities;
using System.Collections.Generic;

namespace AndreasReitberger.Models.CalculationAdditions
{
    public class CalculationProcedureParameter
    {
        #region Properties
        public ProcedureParameter Type { get; set; }
        public double Value { get; set; } = 0;
        public SerializableDictionary<string, double> AdditionalInformation { get; set; } = new SerializableDictionary<string, double>();
        #endregion
    }
}
