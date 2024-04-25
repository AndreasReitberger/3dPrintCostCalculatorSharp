using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AndreasReitberger.Print3d.SQLite.MaterialAdditions
{
    [Table("MaterialAttributes")]
    public partial class Material3dAttribute : ObservableObject, IMaterial3dAttribute
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Material3d))]
        Guid materialId;

        [ObservableProperty]
        string attribute = string.Empty;

        [ObservableProperty]
        double value;
        #endregion

        #region Constructor
        public Material3dAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
