using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite.CalculationAdditions
{
    [Table($"{nameof(CustomAdditionCalculation3dEnhanced)}s")]
    public partial class CustomAdditionCalculation3dEnhanced : ObservableObject, ICustomAdditionCalculation3d
    {
        [ObservableProperty]
        [ForeignKey(typeof(CustomAddition))]
        public partial Guid CustomAdditionId { get; set; }

        [ObservableProperty]
        [ForeignKey(typeof(Calculation3dEnhanced))]
        public partial Guid CalculationId { get; set; }
    }
}
