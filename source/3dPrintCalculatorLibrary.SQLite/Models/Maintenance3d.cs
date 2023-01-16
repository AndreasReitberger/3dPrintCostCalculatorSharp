using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.SQLite.MaintenanceAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table("Maintenances")]
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
        [property: PrimaryKey]
        public Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Printer3d))]
        public Guid printerId;

        [ObservableProperty]
        public string description = string.Empty;
        [ObservableProperty]
        public string category = string.Empty;
        [ObservableProperty]
        public DateTime date;
        [ObservableProperty]
        public double duration;
        [ObservableProperty]
        public double additionalCosts;

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Sparepart> spareparts = new();

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
