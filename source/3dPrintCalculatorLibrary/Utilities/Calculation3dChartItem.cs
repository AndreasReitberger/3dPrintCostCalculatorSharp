using AndreasReitberger.Print3d.Enums;
using System;

namespace AndreasReitberger.Print3d.Utilities
{
    public class Calculation3dChartItem
    {
        #region Properties
        public string Name { get; set; }
        public double Value { get; set; }
        public CalculationAttributeType AttributeType { get; set; }
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        #endregion

        #region Constructor
        public Calculation3dChartItem() { }
        public Calculation3dChartItem(string name, double value)
        {
            Name = name;
            Value = value;
        }
        public Calculation3dChartItem(string name, double value, CalculationAttributeType attributeType)
        {
            Name = name;
            Value = value;
            AttributeType = attributeType;
        }
        #endregion
    }
}
