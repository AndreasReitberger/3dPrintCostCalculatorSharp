using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Core
{
    public partial class Material3dAttribute : ObservableObject, IMaterial3dAttribute
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string attribute = string.Empty;

        [ObservableProperty]
        double value = 0;
        #endregion

        #region Constructor
        public Material3dAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
