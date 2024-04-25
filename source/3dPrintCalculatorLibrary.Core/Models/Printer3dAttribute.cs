using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Core
{
    public partial class Printer3dAttribute : ObservableObject, IPrinter3dAttribute
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string attribute = string.Empty;

        [ObservableProperty]
        double value;
        #endregion

        #region Constructor
        public Printer3dAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
