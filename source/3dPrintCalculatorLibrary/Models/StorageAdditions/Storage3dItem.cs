using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace AndreasReitberger.Print3d.Models.StorageAdditions
{
    public partial class Storage3dItem : ObservableObject, IStorage3dItem
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        Guid materialId;

        [ObservableProperty]
        Material3d? material;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Amount))]
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
