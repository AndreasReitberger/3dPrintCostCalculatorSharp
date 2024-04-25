using AndreasReitberger.Print3d.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Utilities
{
    public partial class Calculation3dChartItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        double value;

        [ObservableProperty]
        CalculationAttributeType attributeType;

        [ObservableProperty]
        CalculationAttributeItem attributeItem;

        [ObservableProperty]
        Guid fileId;

        [ObservableProperty]
        string fileName = string.Empty;
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
