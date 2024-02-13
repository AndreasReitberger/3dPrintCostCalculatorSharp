using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IList<Storage3dTransaction> Transactions { get; }

        public double Amount => GetAvailableAmount();
        #endregion

        #region Ctor
        public Storage3dItem()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods
        public double GetAvailableAmount() => Transactions?.Select(x => x.Amount).Sum() ?? 0;

        #endregion
    }
}
