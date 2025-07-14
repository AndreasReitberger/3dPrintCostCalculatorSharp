using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.StorageAdditions
{
    public partial class Storage3dTransaction : ObservableObject, IStorage3dTransaction
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid? CalculationId { get; set; }

        [ObservableProperty]
        public partial DateTimeOffset DateTime { get; set; }

        [ObservableProperty]
        public partial Storage3dItem? Item { get; set; }

        [ObservableProperty]
        public partial double Amount { get; set; }

        partial void OnAmountChanged(double value)
        {
            IsAddition = value > 0;
        }

        [ObservableProperty]
        public partial Unit Unit { get; set; }

        [ObservableProperty]
        public partial bool IsAddition { get; set; } = false;
        #endregion

        #region Ctor
        public Storage3dTransaction()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
