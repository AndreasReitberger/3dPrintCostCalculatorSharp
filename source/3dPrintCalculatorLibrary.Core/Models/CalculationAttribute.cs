using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(CalculationAttribute)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class CalculationAttribute : ObservableObject, ICalculationAttribute
    {
        #region Properties

#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ForeignKey(typeof(Calculation3dEnhanced))]
#endif
        [ObservableProperty]
        public partial Guid CalculationId { get; set; }

        [ObservableProperty]
        public partial Guid FileId { get; set; } = Guid.Empty;

        [ObservableProperty]
        public partial string FileName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial Guid LinkedId { get; set; } = Guid.Empty;

        [ObservableProperty]
        public partial string Attribute { get; set; } = string.Empty;

        [ObservableProperty]
        public partial CalculationAttributeTarget Target { get; set; }

        [ObservableProperty]
        public partial CalculationAttributeType Type { get; set; }

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
