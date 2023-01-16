using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interface;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite.CalculationAdditions
{
    public partial class ProcedureSpecificAddition : ObservableObject, IProcedureSpecificAddition
    {
        #region Properties
        [ObservableProperty]
        public Printer3dType procedure = Printer3dType.FDM;

        [ObservableProperty]
        public ProcedureSpecificCalculationType calculationType = ProcedureSpecificCalculationType.PerPart;

        [ObservableProperty]
        public double addition;

        [ObservableProperty]
        public bool isPercantageAddition;
        #endregion
    }
}
