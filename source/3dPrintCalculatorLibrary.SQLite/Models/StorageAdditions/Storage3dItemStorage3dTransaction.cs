using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite.StorageAdditions
{
    [Table($"{nameof(Storage3dItemStorage3dTransaction)}s")]
    public partial class Storage3dItemStorage3dTransaction : ObservableObject, IStorage3dItemStorage3dTransaction
    {
        [ObservableProperty]
        [ForeignKey(typeof(Storage3dTransaction))]
        public partial Guid StorageTransactionId { get; set; }

        [ObservableProperty]
        [ForeignKey(typeof(Storage3dItem))]
        public partial Guid StorageItemId { get; set; }
    }
}
