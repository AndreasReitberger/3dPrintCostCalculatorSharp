using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.Models.MaterialAdditions
{
    [Table("MaterialProcedureAttributes")]
    public partial class Material3dProcedureAttribute : ObservableObject, IMaterial3dProcedureAttribute
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
