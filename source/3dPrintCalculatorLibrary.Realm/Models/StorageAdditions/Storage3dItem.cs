using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.StorageAdditions
{
    public partial class Storage3dItem : RealmObject, IStorage3dItem
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid MaterialId { get; set; }

        public Material3d Material { get; set; }

        public double Amount { get; set; }
        #endregion

        #region Ctor
        public Storage3dItem()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
