using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite.WorkstepAdditions
{
    [Table($"{nameof(WorkstepUsageCalculation3d)}s")]
    public partial class WorkstepUsageCalculation3d : ObservableObject, IWorkstepUsageCalculation3d
    {
        [ObservableProperty]
        [ForeignKey(typeof(WorkstepUsage))]
        public partial Guid WorkstepUsageId { get; set; }

        [ObservableProperty]
        [ForeignKey(typeof(Calculation3d))]
        public partial Guid CalculationId { get; set; }
    }
}
