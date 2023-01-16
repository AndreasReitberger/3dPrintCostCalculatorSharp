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
        public Guid id;

        [ObservableProperty]
        public Guid calculationProcedureAttributeId;

        [ObservableProperty]
        public ProcedureParameter type;

        [ObservableProperty]
        public double value = 0;

        [ObservableProperty]
        public List<CalculationProcedureParameterAddition> additions = new();

        #endregion

        #region Constructor
        public CalculationProcedureParameter()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
