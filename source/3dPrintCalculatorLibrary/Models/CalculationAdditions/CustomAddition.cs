using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.CalculationAdditions
{
    public partial class CustomAddition : ObservableObject, ICloneable, ICustomAddition
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties
        [ObservableProperty]
        public Guid id;

        [ObservableProperty]
        public Guid calculationId;

        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public double percentage;

        [ObservableProperty]
        public int order = 0;

        [ObservableProperty]
        public CustomAdditionCalculationType calculationType;
        #endregion

        #region Constructor
        public CustomAddition()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
