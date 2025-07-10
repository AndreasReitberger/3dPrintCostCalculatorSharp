using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite.MaterialAdditions
{
    [Table($"{nameof(Material3dCalculation3d)}s")]
    public partial class Material3dCalculation3d : ObservableObject, IMaterial3dCalculation3d
    {
        [ObservableProperty]
        [ForeignKey(typeof(Material3d))]
        public partial Guid MaterialId { get; set; }

        [ObservableProperty]
        [ForeignKey(typeof(Calculation3d))]
        public partial Guid CalculationId { get; set; }
    }
}
