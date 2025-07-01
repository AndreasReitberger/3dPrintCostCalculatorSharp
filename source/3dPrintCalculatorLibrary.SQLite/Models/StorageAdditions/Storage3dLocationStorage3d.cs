using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite.StorageAdditions
{
    public partial class Storage3dLocationStorage3d : ObservableObject, IStorage3dLocationStorage3d
    {
        [ObservableProperty]
        [property: ForeignKey(typeof(Storage3dLocation))]
        Guid storageLocationId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Storage3d))]
        Guid storageId;
    }
}
