using AndreasReitberger.Enums;

namespace AndreasReitberger.Utilities
{
    public class Calculation3dChartItem
    {
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

        #region Properties
        public string Name { get; set; }
        public double Value { get; set; }
        public CalculationAttributeType AttributeType { get; set; }
        #endregion
    }
}
