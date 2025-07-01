using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite.WorkstepAdditions
{
    public partial class WorkstepUsageCalculation3dEnhanced : ObservableObject, IWorkstepUsageCalculation3dEnhanced
    {
        [ObservableProperty]
        [property: ForeignKey(typeof(WorkstepUsage))]
        Guid workstepUsageId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dEnhanced))]
        Guid calculationId;
    }
}
