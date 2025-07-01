using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.MaterialAdditions
{
    public partial class Material3dProcedureAttribute : ObservableObject, IMaterial3dProcedureAttribute
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid MaterialId { get; set; }

        [ObservableProperty]
        public partial Material3dFamily Family { get; set; }

        [ObservableProperty]
        public partial ProcedureAttribute Attribute { get; set; }

        [ObservableProperty]
        public partial double Value { get; set; }
        #endregion

        #region Constructor
        public Material3dProcedureAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
