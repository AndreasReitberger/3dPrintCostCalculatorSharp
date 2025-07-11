using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite.PrinterAdditions
{
    [Table($"{nameof(Printer3dCalculation3d)}s")]
    public partial class Printer3dCalculation3d : ObservableObject, IPrinter3dCalculation3d
    {
        [ObservableProperty]
        [ForeignKey(typeof(Printer3d))]
        public partial Guid PrinterId { get; set; }

        [ObservableProperty]
        [ForeignKey(typeof(Calculation3d))]
        public partial Guid CalculationId { get; set; }
    }
}
