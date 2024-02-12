using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Core
{
    public partial class ProcedureSpecificAddition : ObservableObject, IProcedureSpecificAddition
    {
        #region Properties
        [ObservableProperty]
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
