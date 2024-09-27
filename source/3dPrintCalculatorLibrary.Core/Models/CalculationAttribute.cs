using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite.CalculationAdditions
{
    [Table($"{nameof(CalculationAttribute)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class CalculationAttribute : ObservableObject, ICalculationAttribute
    {
        #region Properties

        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

        [ObservableProperty]
#if SQL
        [property: ForeignKey(typeof(Calculation3dEnhanced))]
#endif
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
        CalculationAttributeTarget target;

        [ObservableProperty]
        CalculationAttributeType type;

        [ObservableProperty]
        CalculationAttributeItem item = CalculationAttributeItem.Default;

        [ObservableProperty]
        double value = 0;

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
