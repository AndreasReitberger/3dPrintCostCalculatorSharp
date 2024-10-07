using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Storage3dTransaction)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Storage3dTransaction : ObservableObject, IStorage3dTransaction
    {
        #region Properties
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

        [ObservableProperty]
        DateTimeOffset dateTime;

#if SQL
        /*
        [ObservableProperty]
        Guid? calculationId;

        [ObservableProperty]
        [property: ManyToOne(nameof(CalculationId), CascadeOperations = CascadeOperation.All)]
        Calculation3dEnhanced? calculation;
        */

        [ObservableProperty]
        Guid itemId;

        [ObservableProperty]
        [property: ManyToOne(nameof(ItemId), CascadeOperations = CascadeOperation.All)]
        Storage3dItem? item;
#else
        [ObservableProperty]
        IStorage3dItem? item;
#endif

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
