using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Material3dProcedureAttribute)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Material3dProcedureAttribute : ObservableObject, IMaterial3dProcedureAttribute
    {
        #region Properties
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

#if SQL
        [ObservableProperty]
        [property: ForeignKey(typeof(Material3d))]
        Guid materialId;
#endif

        [ObservableProperty]
        Material3dFamily family;

        [ObservableProperty]
        ProcedureAttribute attribute;

        [ObservableProperty]
        double value = 0;
        #endregion

        #region Constructor
        public Material3dProcedureAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
