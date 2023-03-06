using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.CalculationAdditions
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
