using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.MaterialAdditions
{
    public partial class Material3dAttribute : RealmObject, IMaterial3dAttribute
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid MaterialId { get; set; }

        public string Attribute { get; set; } = string.Empty;

        public double Value { get; set; }
        #endregion

        #region Constructor
        public Material3dAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
