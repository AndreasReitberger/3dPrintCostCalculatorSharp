using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Realm.MaterialAdditions;
using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Realm
{
    public partial class Print3dInfo : RealmObject, IPrint3dInfo, ICloneable
    {

        #region Properties

        [PrimaryKey]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        [Obsolete("Use Calculation3dEnhanced instead")]
        public Guid CalculationId { get; set; }

        public Guid CalculationEnhancedId { get; set; }

        public Guid FileId { get; set; }

        public File3d File { get; set; }

        public Guid PrinterId { get; set; }

        public Printer3d Printer { get; set; }

        public IList<Item3dUsage> Items { get; }

        public IList<Material3dUsage> MaterialUsages { get; }
        #endregion

        #region Constructor

        public Print3dInfo()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object obj)
        {
            if (obj is not Printer3d item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        public object Clone() => MemberwiseClone();

        #endregion

    }
}
