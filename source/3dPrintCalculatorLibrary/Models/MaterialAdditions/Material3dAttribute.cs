using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.MaterialAdditions
{
    public partial class Material3dAttribute : ObservableObject, IMaterial3dAttribute
    {
        #region Properties
        [ObservableProperty]
        public Guid id;

        [ObservableProperty]
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
