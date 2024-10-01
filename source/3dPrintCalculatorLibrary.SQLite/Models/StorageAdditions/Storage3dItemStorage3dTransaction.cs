﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite.StorageAdditions
{
    public partial class Storage3dItemStorage3dTransaction : ObservableObject
    {
        [ObservableProperty]
        [property: ForeignKey(typeof(Storage3dTransaction))]
        Guid storageTransactionId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Storage3dItem))]
        Guid storageItemId;
    }
}
