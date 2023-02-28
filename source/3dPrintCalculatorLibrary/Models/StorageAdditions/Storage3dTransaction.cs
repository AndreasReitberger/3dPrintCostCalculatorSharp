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
        DateTime dateTime;

        [ObservableProperty]
        Storage3dItem item;

        [ObservableProperty]
        double amount;

        [ObservableProperty]
        Unit unit;
        #endregion

        #region Ctor
        public Storage3dTransaction()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
