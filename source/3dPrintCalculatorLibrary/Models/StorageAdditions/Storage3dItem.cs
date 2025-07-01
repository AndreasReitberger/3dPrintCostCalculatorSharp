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
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial Guid MaterialId { get; set; }

        [ObservableProperty]
        public partial Material3d? Material { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Amount))]
        public partial ObservableCollection<Storage3dTransaction> Transactions { get; set; } = [];

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
