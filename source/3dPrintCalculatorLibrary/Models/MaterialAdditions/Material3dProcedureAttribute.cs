using AndreasReitberger.Enums;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Models.MaterialAdditions
{
    [Table("MaterialProcedureAttributes")]
    public class Material3dProcedureAttribute
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }
        [ForeignKey(typeof(Material3d))]
        public Guid MaterialId { get; set; }

        public Material3dFamily Family { get; set; }

        public ProcedureAttribute Attribute { get; set; }

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
