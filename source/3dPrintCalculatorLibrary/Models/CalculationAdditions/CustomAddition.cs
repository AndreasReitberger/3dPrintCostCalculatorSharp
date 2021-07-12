using AndreasReitberger.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AndreasReitberger.Models.CalculationAdditions
{
    public class CustomAddition : ICloneable
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties
        public string Name { get; set; }
        public double Percentage { get; set; }
        public int Order { get; set; } = 0;
        public CustomAdditionCalculationType CalculationType { get; set; }
        #endregion

        #region Constructor
        public CustomAddition() { }
        #endregion
    }
}
