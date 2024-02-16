using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Core
{
    public partial class Storage3dTransaction : ObservableObject, IStorage3dTransaction
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        DateTimeOffset dateTime;

        [ObservableProperty]
        IStorage3dItem? item;

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
