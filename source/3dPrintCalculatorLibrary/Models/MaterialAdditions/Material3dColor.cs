using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Models.MaterialAdditions
{
    [Table("MaterialColors")]
    public class Material3dColor
    {
        #region Properties 
        [PrimaryKey]
        public Guid Id
        { get; set; }
        [ForeignKey(typeof(Material3d))]
        public Guid MaterialId 
        { get; set; }
        public string Name
        { get; set; }
        public string HexColorCode
        { get; set; }
        public string SKU
        { get; set; }
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
        public override string ToString()
        {
            return $"{Name} (#{HexColorCode})";
        }
        public override bool Equals(object obj)
        {
            if (obj is not Material3dColor item)
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
