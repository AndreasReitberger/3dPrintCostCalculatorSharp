using AndreasReitberger.Print3d.Interfaces;
using Newtonsoft.Json;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.FileAdditions
{
    public partial class File3dUsage : RealmObject, ICloneable, IFile3dUsage
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

        public Guid FileId { get; set; }

        public File3d? File { get; set; }

        public int Quantity { get; set; } = 1;

        public bool MultiplyPrintTimeWithQuantity { get; set; } = true;

        public double PrintTimeQuantityFactor { get; set; } = 1;

        #endregion

        #region Constructor
        public File3dUsage()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not File3dUsage item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}
