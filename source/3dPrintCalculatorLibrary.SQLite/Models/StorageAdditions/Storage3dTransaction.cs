using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
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
        [property: ManyToOne(nameof(CalculationId), CascadeOperations = CascadeOperation.All)]
        Calculation3d? calculation;

        [ObservableProperty]
        DateTimeOffset dateTime;
        /*
        [ObservableProperty]
        Guid storageItemId;

        [ObservableProperty]
        [property: ManyToOne(nameof(StorageItemId), CascadeOperations = CascadeOperation.All)]
        Storage3dItem item;
        */

        [ObservableProperty]
        Unit unit;

        [ObservableProperty]
        double amount;

        #endregion

        #region Ctor
        public Storage3dTransaction()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
