using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite.StorageAdditions
{
    [Table($"{nameof(Storage3dItemStorage3dLocation)}s")]
    public partial class Storage3dItemStorage3dLocation : ObservableObject, IStorage3dItemStorage3dLocation
    {
        [ObservableProperty]
        [ForeignKey(typeof(Storage3dLocation))]
        public partial Guid StorageLocationId { get; set; }

        [ObservableProperty]
        [ForeignKey(typeof(Storage3dItem))]
        public partial Guid StorageItemId { get; set; }
    }
}
