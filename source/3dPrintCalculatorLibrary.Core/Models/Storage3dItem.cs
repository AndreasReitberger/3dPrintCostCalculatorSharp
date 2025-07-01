using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
using AndreasReitberger.Print3d.SQLite.StorageAdditions;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Storage3dItem)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Storage3dItem : ObservableObject, IStorage3dItem
    {
        #region Properties
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

        [ObservableProperty]
        string name = string.Empty;

#if SQL
        [ObservableProperty]
        Guid materialId;

        [ObservableProperty]
        [property: ManyToOne(nameof(MaterialId), CascadeOperations = CascadeOperation.All)]
        Material3d? material;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Amount))]
        [property: ManyToMany(typeof(Storage3dItemStorage3dTransaction), CascadeOperations = CascadeOperation.All)]
        List<Storage3dTransaction> transactions = [];
#else
        [ObservableProperty]
        IMaterial3d? material;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Amount))]
        IList<IStorage3dTransaction> transactions = [];
#endif
        public double Amount => GetAvailableAmount();
        #endregion

        #region Ctor
        public Storage3dItem()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods
        public double GetAvailableAmount() => Transactions?.Select(x => x.Amount).Sum() ?? 0;

        #endregion
    }
}
