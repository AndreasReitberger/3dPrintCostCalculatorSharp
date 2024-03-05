using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Print3d.Models.CalculationAdditions
{
    public partial class CalculationAttribute : ObservableObject, ICalculationAttribute
    {
        #region Properties

        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        Guid calculationEnhancedId;

        [ObservableProperty]
        Guid fileId = Guid.Empty;

        [ObservableProperty]
        string fileName;

        [ObservableProperty]
        Guid linkedId = Guid.Empty;

        [ObservableProperty]
        string attribute;

        [ObservableProperty]
        CalculationAttributeType type;

        [ObservableProperty]
        CalculationAttributeItem item = CalculationAttributeItem.Default;

        [ObservableProperty]
        double value;

        [ObservableProperty]
        bool isPercentageValue = false;

        [ObservableProperty]
        bool applyPerFile = false;

        [ObservableProperty]
        bool skipForCalculation = false;

        [ObservableProperty]
        bool skipForMargin = false;
        #endregion

        #region Constructor
        public CalculationAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
