using AndreasReitberger.Print3d.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Utilities
{
    public partial class Calculation3dChartItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial double Value { get; set; }

        [ObservableProperty]
        public partial CalculationAttributeType AttributeType { get; set; }

        [ObservableProperty]
        public partial CalculationAttributeItem AttributeItem { get; set; }

        [ObservableProperty]
        public partial Guid FileId { get; set; }

        [ObservableProperty]
        public partial string FileName { get; set; } = string.Empty;
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
