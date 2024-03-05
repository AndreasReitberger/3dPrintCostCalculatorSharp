using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.MaintenanceAdditions
{
    public partial class Sparepart : RealmObject, ISparepart
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid MaintenanceId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Partnumber { get; set; } = string.Empty;

        public double Costs { get; set; }
        #endregion

        #region Constructor
        public Sparepart()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Sparepart item)
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
