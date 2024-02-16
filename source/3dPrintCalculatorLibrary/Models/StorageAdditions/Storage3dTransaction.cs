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
        Guid id;

        [ObservableProperty]
        Guid? calculationId;

        [ObservableProperty]
        DateTimeOffset dateTime;

        [ObservableProperty]
        Storage3dItem item;

        [ObservableProperty]
        double amount;
        partial void OnAmountChanged(double value)
        {
            IsAddition = value > 0;
        }

        [ObservableProperty]
        Unit unit;

        [ObservableProperty]
        bool isAddition = false;
        #endregion

        #region Ctor
        public Storage3dTransaction()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
