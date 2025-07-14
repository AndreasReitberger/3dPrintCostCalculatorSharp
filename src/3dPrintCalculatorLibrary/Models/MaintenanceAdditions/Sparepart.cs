using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Print3d.Models.MaintenanceAdditions
{
    public partial class Sparepart : ObservableObject, ISparepart
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid MaintenanceId { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Partnumber { get; set; } = string.Empty;

        [ObservableProperty]
        public partial double Costs { get; set; }
        #endregion

        #region Constructor
        public Sparepart()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not Sparepart item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}
