using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Models.CalculationAdditions
{
    public partial class CalculationProcedureAttribute : ObservableObject, ICalculationProcedureAttribute
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid CalculationEnhancedId { get; set; }

        [ObservableProperty]
        public partial Material3dFamily Family { get; set; }

        [ObservableProperty]
        public partial ProcedureAttribute Attribute { get; set; }

        [ObservableProperty]
        public partial List<CalculationProcedureParameter> Parameters { get; set; } = [];

        [ObservableProperty]
        public partial CalculationLevel Level { get; set; } = CalculationLevel.Printer;

        [ObservableProperty]
        public partial bool PerFile { get; set; } = false;

        [ObservableProperty]
        public partial bool PerPiece { get; set; } = false;
        #endregion

        #region Constructor
        public CalculationProcedureAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
