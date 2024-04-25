using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.MaterialAdditions
{
    public partial class Material3dType : RealmObject, IMaterial3dType
    {
        #region Properties 
        [PrimaryKey]
        public Guid Id { get; set; }

        public Material3dFamily Family
        {
            get => (Material3dFamily)FamilyId;
            set { FamilyId = (int)value; }
        }
        public int FamilyId { get; set; }

        public string Material { get; set; } = string.Empty;

        public string Polymer { get; set; } = string.Empty;
        #endregion

        #region Constructors
        public Material3dType()
        {
            Id = Guid.NewGuid();
        }
        public Material3dType(Material3dFamily type, string material)
        {
            Id = Guid.NewGuid();
            Family = type;
            Material = material;
        }
        public Material3dType(Guid id, Material3dFamily type, string material)
        {
            Id = id;
            Family = type;
            Material = material;
        }
        public Material3dType(Guid id, Material3dFamily type, string material, string polymer)
        {
            Id = id;
            Family = type;
            Material = material;
            Polymer = polymer;
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return string.IsNullOrEmpty(Family.ToString()) ? Material : string.Format("{0} ({1})", Material, Family.ToString());
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Material3dType item)
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
