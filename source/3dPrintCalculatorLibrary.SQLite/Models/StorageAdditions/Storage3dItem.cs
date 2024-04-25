using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;

namespace AndreasReitberger.Print3d.SQLite.StorageAdditions
{
    [Table("StorageItems")]
    public partial class Storage3dItem : ObservableObject, IStorage3dItem
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        Guid materialId;

        [ObservableProperty]
        [property: ManyToOne(nameof(MaterialId), CascadeOperations = CascadeOperation.All)]
        Material3d? material;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Amount))]
        [property: ManyToMany(typeof(Storage3dItemStorage3dTransaction), CascadeOperations = CascadeOperation.All)]
        ObservableCollection<Storage3dTransaction> transactions = [];

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
