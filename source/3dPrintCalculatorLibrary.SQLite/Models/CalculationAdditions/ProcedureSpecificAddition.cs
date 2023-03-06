using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace AndreasReitberger.Print3d.SQLite.CalculationAdditions
{
    public partial class ProcedureSpecificAddition : ObservableObject, IProcedureSpecificAddition
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
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
