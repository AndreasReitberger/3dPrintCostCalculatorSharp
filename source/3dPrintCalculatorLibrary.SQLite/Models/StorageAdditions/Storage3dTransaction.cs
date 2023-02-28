using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AndreasReitberger.Print3d.SQLite.StorageAdditions
{
    [Table("StorageTransactions")]
    public partial class Storage3dTransaction : ObservableObject, IStorage3dTransaction
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        Guid? calculationId;

        [ObservableProperty]
        [property: ManyToOne(nameof(CalculationId))]
        Calculation3d? calculation;

        [ObservableProperty]
        DateTime dateTime;

        [ObservableProperty]
        Guid storageItemId;

        [ObservableProperty]
        [property: ManyToOne(nameof(StorageItemId))]
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
