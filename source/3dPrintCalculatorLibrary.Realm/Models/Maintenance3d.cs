using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Realm.MaintenanceAdditions;
using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AndreasReitberger.Print3d.Realm
{
    public partial class Maintenance3d : RealmObject, ICloneable, IMaintenance3d
    {
        #region Clone
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion

        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid PrinterId { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public DateTimeOffset Date { get; set; }

        public double Duration { get; set; }

        public double AdditionalCosts { get; set; }

        [Ignored]
        public double Costs => Spareparts?.Sum(sparepart => sparepart.Costs) ?? 0 + AdditionalCosts;
        #endregion

        #region Collections
        public IList<Sparepart> Spareparts { get; } = [];
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
