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
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

        [ObservableProperty]
        Printer3dType procedure = Printer3dType.FDM;

        [ObservableProperty]
        ProcedureSpecificCalculationType calculationType = ProcedureSpecificCalculationType.PerPart;

        [ObservableProperty]
        double addition;

        [ObservableProperty]
        bool isPercantageAddition;
        #endregion
    }
}
