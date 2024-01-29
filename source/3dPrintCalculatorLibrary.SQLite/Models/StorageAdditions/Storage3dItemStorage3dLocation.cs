using CommunityToolkit.Mvvm.ComponentModel;
using SQLiteNetExtensions.Attributes;

namespace AndreasReitberger.Print3d.SQLite.StorageAdditions
{
    public partial class Storage3dItemStorage3dLocation : ObservableObject
    {
        [ObservableProperty]
        [property: ForeignKey(typeof(Storage3dLocation))]
        Guid storageLocationId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Storage3dItem))]
        Guid storageItemId;
    }
}
