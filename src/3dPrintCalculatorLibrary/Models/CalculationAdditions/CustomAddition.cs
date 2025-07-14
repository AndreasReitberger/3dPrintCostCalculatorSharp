using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.CalculationAdditions
{
    public partial class CustomAddition : ObservableObject, ICloneable, ICustomAddition
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid CalculationId { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial double Percentage { get; set; }

        [ObservableProperty]
        public partial int Order { get; set; } = 0;

        [ObservableProperty]
        public partial CustomAdditionCalculationType CalculationType { get; set; }
        #endregion

        #region Constructor
        public CustomAddition()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

    }
}
