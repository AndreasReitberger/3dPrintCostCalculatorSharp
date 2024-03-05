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
        Calculation3dEnhanced? calculation;

        [ObservableProperty]
        DateTimeOffset dateTime;

        [ObservableProperty]
        Unit unit;

        [ObservableProperty]
        double amount;
        partial void OnAmountChanged(double value)
        {
            IsAddition = value > 0;
        }

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
