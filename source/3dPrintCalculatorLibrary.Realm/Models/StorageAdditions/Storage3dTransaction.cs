using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.StorageAdditions
{
    public partial class Storage3dTransaction : RealmObject, IStorage3dTransaction
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid? CalculationId { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public Storage3dItem Item { get; set; }

        public double Amount { get; set; }

        public Unit Unit
        {
            get => (Unit)UnitId;
            set { UnitId = (int)value; }
        }
        public int UnitId { get; set; }
        #endregion

        #region Ctor
        public Storage3dTransaction()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
