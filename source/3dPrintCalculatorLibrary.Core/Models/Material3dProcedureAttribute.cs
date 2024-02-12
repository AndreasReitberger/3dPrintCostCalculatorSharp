using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Core
{
    public partial class Material3dProcedureAttribute : ObservableObject, IMaterial3dProcedureAttribute
    {
        #region Properties
        [ObservableProperty]
        Guid id;

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
