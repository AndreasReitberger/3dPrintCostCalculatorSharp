using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Realm.CalculationAdditions
{
    public partial class CalculationProcedureParameterAddition : ObservableObject, ICalculationProcedureParameterAddition
    {
        #region Properties
        [ObservableProperty]
        public Guid id;

        [ObservableProperty]
        public Guid calculationProcedureParameterId;

        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public double value = 0;

        #endregion

        #region Constructor
        public CalculationProcedureParameterAddition()
        {
            Id = Guid.NewGuid();
        }
        public CalculationProcedureParameterAddition(string name, double value)
        {
            Id = Guid.NewGuid();
            Name = name;
            Value = value;
        }
        #endregion
    }
}
