using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Realm.MaterialAdditions
{
    public partial class Material3dProcedureAttribute : ObservableObject, IMaterial3dProcedureAttribute
    {
        #region Properties
        [ObservableProperty]
        public Guid id;

        [ObservableProperty]
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
