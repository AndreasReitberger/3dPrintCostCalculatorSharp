using AndreasReitberger.Print3d.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.Models.MaterialAdditions
{
    [Table("MaterialAttributes")]
    public partial class Material3dAttribute : ObservableObject, IMaterial3dAttribute
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        public Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Material3d))]
        public Guid materialId;

        [ObservableProperty]
        public string attribute;

        [ObservableProperty]
        public double value;
        #endregion

        #region Constructor
        public Material3dAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
