using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.MaterialAdditions
{
    public partial class Material3dProcedureAttribute : RealmObject, IMaterial3dProcedureAttribute
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid MaterialId { get; set; }

        public Material3dFamily Family
        {
            get => (Material3dFamily)FamilyId;
            set { FamilyId = (int)value; }
        }
        public int FamilyId { get; set; }

        public ProcedureAttribute Attribute
        {
            get => (ProcedureAttribute)AttributeId;
            set { AttributeId = (int)value; }
        }
        public int AttributeId { get; set; }

        public double Value { get; set; }
        #endregion

        #region Constructor
        public Material3dProcedureAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
