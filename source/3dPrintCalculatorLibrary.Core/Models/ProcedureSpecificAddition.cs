using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(ProcedureSpecificAddition)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class ProcedureSpecificAddition : ObservableObject, IProcedureSpecificAddition
    {
        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Printer3dType Procedure { get; set; } = Printer3dType.FDM;

        [ObservableProperty]
        public partial ProcedureSpecificCalculationType CalculationType { get; set; } = ProcedureSpecificCalculationType.PerPart;

        [ObservableProperty]
        public partial double Addition { get; set; }

        [ObservableProperty]
        public partial bool IsPercantageAddition { get; set; }
        #endregion
    }
}
