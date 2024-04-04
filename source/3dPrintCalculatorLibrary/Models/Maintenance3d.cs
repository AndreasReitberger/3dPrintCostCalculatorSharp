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
        Guid id;

        [ObservableProperty]
        Guid printerId;

        [ObservableProperty]
        string description = string.Empty;

        [ObservableProperty]
        string category = string.Empty;

        [ObservableProperty]
        DateTimeOffset date;

        [ObservableProperty]
        double duration;

        [ObservableProperty]
        double additionalCosts;

        [ObservableProperty]
        List<Sparepart> spareparts = new();

        public double Costs => Spareparts?.Sum(sparepart => sparepart.Costs) ?? 0 + AdditionalCosts;
        #endregion

        #region Constructor
        public Maintenance3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return this.Description;
        }
        public override bool Equals(object obj)
        {
            if (obj is not Maintenance3d item)
                return false;
            return this.Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        #endregion
    }
}
