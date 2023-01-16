using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.SQLite.MaterialAdditions
{
    [Table("MaterialProcedureAttributes")]
    public partial class Material3dProcedureAttribute : ObservableObject, IMaterial3dProcedureAttribute
    {
        #region Properties

        [ObservableProperty]
        [property: PrimaryKey]
        public Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Material3d))]
        public Guid materialId;

        [ObservableProperty]
        public Material3dFamily family;

        [ObservableProperty]
        public ProcedureAttribute attribute;

        [ObservableProperty]
        public double value;
        #endregion

        #region Constructor
        public Material3dProcedureAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
