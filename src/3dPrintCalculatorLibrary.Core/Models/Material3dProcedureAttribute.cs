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
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ObservableProperty]
        [ForeignKey(typeof(Material3d))]
        public partial Guid MaterialId { get; set; }
#endif

        [ObservableProperty]
        public partial Material3dFamily Family { get; set; }

        [ObservableProperty]
        public partial ProcedureAttribute Attribute { get; set; }

        [ObservableProperty]
        public partial double Value { get; set; } = 0;
        #endregion

        #region Constructor
        public Material3dProcedureAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
