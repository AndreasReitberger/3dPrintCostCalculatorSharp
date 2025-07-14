using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite.WorkstepAdditions
{
    [Table($"{nameof(WorkstepUsageCalculation3dEnhanced)}s")]
    public partial class WorkstepUsageCalculation3dEnhanced : ObservableObject, IWorkstepUsageCalculation3d
    {
        [ObservableProperty]
        [property: ForeignKey(typeof(WorkstepUsage))]
        Guid workstepUsageId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dEnhanced))]
        Guid calculationId;
    }
}
