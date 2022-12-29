using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.Models.MaterialAdditions
{
    [Table("MaterialAttributes")]
    public class Material3dAttribute
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }
        [ForeignKey(typeof(Material3d))]
        public Guid MaterialId { get; set; }
        public string Attribute { get; set; }
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
