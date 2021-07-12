using AndreasReitberger.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AndreasReitberger.Models.CalculationAdditions
{
    public class CalculationAttribute
    {
        #region Properties
        public Guid LinkedId { get; set; } = Guid.Empty;
        public string Attribute { get; set; }
        public CalculationAttributeType Type { get; set; }
        public double Value { get; set; }
        //public CalculationAttributeTarget Target { get; set; }
        public bool IsPercentageValue { get; set; } = false;
        public bool SkipForCalculation { get; set; } = false;
        #endregion
    }
}
