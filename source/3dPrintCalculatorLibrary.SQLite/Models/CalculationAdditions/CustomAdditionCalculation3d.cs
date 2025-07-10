using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite.CalculationAdditions
{
    [Table($"{nameof(CustomAdditionCalculation3d)}s")]
    public partial class CustomAdditionCalculation3d : ObservableObject, ICustomAdditionCalculation3d
    {
        [ObservableProperty]
        [ForeignKey(typeof(CustomAddition))]
        public partial Guid CustomAdditionId { get; set; }

        [ObservableProperty]
        [ForeignKey(typeof(Calculation3d))]
        public partial Guid CalculationId { get; set; }
    }
}
