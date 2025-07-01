using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite.CalculationAdditions
{
    public partial class CustomAdditionCalculation3dEnhanced : ObservableObject, ICustomAdditionCalculation3dEnhanced
    {
        [ObservableProperty]
        [ForeignKey(typeof(CustomAddition))]
        public partial Guid CustomAdditionId { get; set; }

        [ObservableProperty]
        [ForeignKey(typeof(Calculation3dEnhanced))]
        public partial Guid CalculationId { get; set; }
    }
}
