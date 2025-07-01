using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite.CalculationAdditions
{
    public partial class CustomAdditionCalculation3dEnhanced : ObservableObject, ICustomAdditionCalculation3dEnhanced
    {
        [ObservableProperty]
        [property: ForeignKey(typeof(CustomAddition))]
        Guid customAdditionId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dEnhanced))]
        Guid calculationId;
    }
}
