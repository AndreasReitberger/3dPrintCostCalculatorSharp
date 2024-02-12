using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Core
{
    public partial class CalculationAttribute : ObservableObject, ICalculationAttribute
    {
        #region Properties

        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        Guid calculationId;

        [ObservableProperty]
        Guid fileId = Guid.Empty;

        [ObservableProperty]
        string fileName = string.Empty;

        [ObservableProperty]
        Guid linkedId = Guid.Empty;

        [ObservableProperty]
        string attribute = string.Empty;

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
