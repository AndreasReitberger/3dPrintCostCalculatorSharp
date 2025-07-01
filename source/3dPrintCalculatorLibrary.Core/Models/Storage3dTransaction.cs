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
#if SQL
        [property: PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial DateTimeOffset DateTime { get; set; }

#if SQL
        /*
        [ObservableProperty]
        Guid? calculationId;

        [ObservableProperty]
        [property: ManyToOne(nameof(CalculationId), CascadeOperations = CascadeOperation.All)]
        Calculation3dEnhanced? calculation;
        */

        [ObservableProperty]
        public partial Guid ItemId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(ItemId), CascadeOperations = CascadeOperation.All)]
        public partial Storage3dItem? Item { get; set; }
#else
        [ObservableProperty]
        public partial IStorage3dItem? Item { get; set; }
#endif

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
