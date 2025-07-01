using AndreasReitberger.Print3d.Interfaces;
using Newtonsoft.Json;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.MaterialAdditions
{
    public partial class Material3dUsage : RealmObject, ICloneable, IMaterial3dUsage
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid PrintInfoId { get; set; }

        public Guid MaterialId { get; set; }

        public Material3d? Material { get; set; }

        public double PercentageValue { get; set; } = 1;

        public double Percentage => PercentageValue * 100;

        #endregion

        #region Constructor
        public Material3dUsage()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not Material3dUsage item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}
