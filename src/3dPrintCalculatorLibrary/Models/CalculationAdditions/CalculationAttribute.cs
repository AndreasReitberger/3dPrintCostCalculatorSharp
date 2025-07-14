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
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid CalculationEnhancedId { get; set; }

        [ObservableProperty]
        public partial Guid FileId { get; set; } = Guid.Empty;

        [ObservableProperty]
        public partial string FileName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial Guid LinkedId { get; set; } = Guid.Empty;

        [ObservableProperty]
        public partial string Attribute { get; set; } = string.Empty;

        [ObservableProperty]
        public partial CalculationAttributeType Type { get; set; }

        [ObservableProperty]
        public partial CalculationAttributeTarget Target { get; set; }

        [ObservableProperty]
        public partial CalculationAttributeItem Item { get; set; } = CalculationAttributeItem.Default;

        [ObservableProperty]
        public partial double Value { get; set; } = 0;

        [ObservableProperty]
        public partial bool IsPercentageValue { get; set; } = false;

        [ObservableProperty]
        public partial bool ApplyPerFile { get; set; } = false;

        [ObservableProperty]
        public partial bool SkipForCalculation { get; set; } = false;

        [ObservableProperty]
        public partial bool SkipForMargin { get; set; } = false;
        #endregion

        #region Constructor
        public CalculationAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
