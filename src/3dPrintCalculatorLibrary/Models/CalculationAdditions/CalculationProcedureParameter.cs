using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Models.CalculationAdditions
{
    public partial class CalculationProcedureParameter : ObservableObject, ICalculationProcedureParameter
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid CalculationProcedureAttributeId { get; set; }

        [ObservableProperty]
        public partial ProcedureParameter Type { get; set; }

        [ObservableProperty]
        public partial double Value { get; set; } = 0;

        [ObservableProperty]
        public partial List<CalculationProcedureParameterAddition> Additions { get; set; } = new();

        #endregion

        #region Constructor
        public CalculationProcedureParameter()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
