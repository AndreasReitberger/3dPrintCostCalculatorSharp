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
        public Guid id;

        [ObservableProperty]
        public Guid calculationId;

        [ObservableProperty]
        public Material3dFamily family;

        [ObservableProperty]
        public ProcedureAttribute attribute;

        [ObservableProperty]
        public List<CalculationProcedureParameter> parameters = new();

        [ObservableProperty]
        public CalculationLevel level = CalculationLevel.Printer;
        #endregion

        #region Constructor
        public CalculationProcedureAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
