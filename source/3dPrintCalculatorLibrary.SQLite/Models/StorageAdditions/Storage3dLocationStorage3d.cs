using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite.StorageAdditions
{
    public partial class Storage3dLocationStorage3d : ObservableObject, IStorage3dLocationStorage3d
    {
        [ObservableProperty]
        [ForeignKey(typeof(Storage3dLocation))]
        public partial Guid StorageLocationId { get; set; }

        [ObservableProperty]
        [ForeignKey(typeof(Storage3d))]
        public partial Guid StorageId { get; set; }
    }
}
