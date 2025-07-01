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
