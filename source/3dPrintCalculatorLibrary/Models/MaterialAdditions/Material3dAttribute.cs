using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Models.MaterialAdditions
{
    public partial class Material3dAttribute : ObservableObject, IMaterial3dAttribute
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid MaterialId { get; set; }

        [ObservableProperty]
        public partial string Attribute { get; set; } = string.Empty;

        [ObservableProperty]
        public partial double Value { get; set; }
        #endregion

        #region Constructor
        public Material3dAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
