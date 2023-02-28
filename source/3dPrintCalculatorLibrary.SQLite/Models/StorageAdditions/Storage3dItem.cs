using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

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
        [property: ForeignKey(typeof(Storage3d))]
        Guid storageId;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        public Guid materialId;

        [ObservableProperty]
        [property: ManyToOne(nameof(MaterialId))]
        Material3d material;

        [ObservableProperty]
        double amount;
        #endregion

        #region Ctor
        public Storage3dItem()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
