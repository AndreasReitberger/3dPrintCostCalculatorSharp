using AndreasReitberger.Print3d.Interfaces;
using Newtonsoft.Json;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.MaterialAdditions
{
    public partial class Material3dColor : RealmObject, IMaterial3dColor
    {
        #region Properties 
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid MaterialId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string HexColorCode { get; set; } = string.Empty;

        #endregion

        #region Constructors
        public Material3dColor()
        {
            Id = Guid.NewGuid();
        }
        public Material3dColor(string name, string hexColorCode)
        {
            Id = Guid.NewGuid();
            Name = name;
            HexColorCode = hexColorCode;
        }
        public Material3dColor(Guid id, string name, string hexColorCode)
        {
            Id = id;
            Name = name;
            HexColorCode = hexColorCode;
        }
        #endregion

        #region Override
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not Material3dColor item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}
