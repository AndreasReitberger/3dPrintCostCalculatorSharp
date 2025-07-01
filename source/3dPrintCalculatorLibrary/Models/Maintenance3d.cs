using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Models.MaintenanceAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AndreasReitberger.Print3d.Models
{
    public partial class Maintenance3d : ObservableObject, ICloneable, IMaintenance3d
    {
        #region Clone
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion

        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid PrinterId { get; set; }

        [ObservableProperty]
        public partial string Description { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Category { get; set; } = string.Empty;

        [ObservableProperty]
        public partial DateTimeOffset Date { get; set; }

        [ObservableProperty]
        public partial double Duration { get; set; }

        [ObservableProperty]
        public partial double AdditionalCosts { get; set; }

        [ObservableProperty]
        public partial List<Sparepart> Spareparts { get; set; } = [];

        public double Costs => Spareparts?.Sum(sparepart => sparepart.Costs) ?? 0 + AdditionalCosts;
        #endregion

        #region Constructor
        public Maintenance3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not Maintenance3d item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();
        
        #endregion
    }
}
